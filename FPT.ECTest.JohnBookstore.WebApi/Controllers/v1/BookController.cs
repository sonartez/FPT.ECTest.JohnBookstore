using FPT.ECTest.JohnBookstore.Application.Exceptions;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetBookByISBN;
using FPT.ECTest.JohnBookstore.Application.Features.Shops.Commands.CreateShop;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FPT.ECTest.JohnBookstore.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BookController : BaseApiController
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        public BookController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: api/<controller>
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllBooksParameter filter)
        {
            return Ok(await Mediator.Send(new GetAllBooksQuery() { PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }


        // GET api/<controller>/5
        [HttpGet("{isbnCode}")]
        public async Task<IActionResult> Get(string isbnCode)
        {
            return Ok(await Mediator.Send(new GetBookByISBNQuery { ISBNCode = isbnCode }));
        }

        // GET api/<controller>/search
        [HttpGet("Search")]
        public async Task<IActionResult> Search([FromQuery] FindBooksParameter filter)
        {
            return Ok(await Mediator.Send(new FindBooksQuery() { FindText = filter.FindText, PageSize = filter.PageSize, PageNumber = filter.PageNumber }));
        }

        // POST api/<controller>
        [HttpPost("PlaceOrder")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(OrderBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HttpPost("ImportBooks/{shopCode}")]
        [Authorize]
        public async Task<IActionResult> ImportBooks(IFormFile xmlFile, string shopCode)
        {
            ImportBookCommand importBookCommand = new ImportBookCommand();

            if (string.IsNullOrWhiteSpace(_webHostEnvironment.WebRootPath))
            {
                _webHostEnvironment.WebRootPath = System.IO.Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
            }

            var uploads = Path.Combine(_webHostEnvironment.WebRootPath, shopCode);
            Directory.CreateDirectory(uploads);

            var filePath = Path.Combine(uploads, xmlFile.FileName).ToString();

            if (xmlFile.ContentType.Equals("application/xml") || xmlFile.ContentType.Equals("text/xml"))
            {
                try
                {
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await xmlFile.CopyToAsync(fileStream);
                        fileStream.Dispose();
                        XDocument xDoc = XDocument.Load(filePath);
                        List<XMLBook> books = xDoc.Descendants("book").Select(book =>
                         new XMLBook
                         {
                             ISBNCode = book.Element("isbn").Value,
                             Author = book.Element("author").Value,
                             BookName = book.Element("name").Value,
                             Price = Decimal.Parse(book.Element("price").Value),
                             InStock = Int32.Parse(book.Element("instock").Value),
                             Description = book.Element("description").Value,

                         }).ToList();
                        importBookCommand.Books = books;
                    }
                }
                catch (Exception e)
                {
                    throw new ApiException($"Converting fail.");
                }
            }
            else
            {
                throw new ApiException($"Uploading fail.");
            }

            importBookCommand.ShopCode = shopCode;
            return Ok(await Mediator.Send(importBookCommand));
        }

        // POST api/<controller>
        [HttpPost("CreateShop")]
        [Authorize]
        public async Task<IActionResult> CreateShop(CreateShopCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

    }
}
