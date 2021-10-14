using FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetBookByISBN;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FPT.ECTest.JohnBookstore.WebApi.Controllers.v1
{
    [ApiVersion("1.0")]
    public class BookController : BaseApiController
    {
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
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Post(CreateBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        // POST api/<controller>
        [HttpPost("PlaceOrder")]
        [Authorize]
        public async Task<IActionResult> PlaceOrder(OrderBookCommand command)
        {
            return Ok(await Mediator.Send(command));
        }

        /* // PUT api/<controller>/5
         [HttpPut("{id}")]
         [Authorize]
         public async Task<IActionResult> Put(int id, UpdateProductCommand command)
         {
             if (id != command.Id)
             {
                 return BadRequest();
             }
             return Ok(await Mediator.Send(command));
         }

         // DELETE api/<controller>/5
         [HttpDelete("{id}")]
         [Authorize]
         public async Task<IActionResult> Delete(int id)
         {
             return Ok(await Mediator.Send(new DeleteProductByIdCommand { Id = id }));
         }*/
    }
}
