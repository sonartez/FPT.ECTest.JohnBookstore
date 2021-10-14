using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Exceptions;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook
{
    public partial class ImportBookCommand : IRequest<Response<int>>
    {

        public string ShopCode { get; set; }
        public List<XMLBook> Books { get; set; }

    }

    public class XMLBook
    {
        public string ISBNCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public int InStock { get; set; }
    }

    public class ImportBookCommandHandler : IRequestHandler<ImportBookCommand, Response<int>>
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IShopRepositoryAsync _shopRepository;
        private readonly IShopBookRepositoryAsync _shopBookRepositoryAsync;
        private readonly IMapper _mapper;
        public ImportBookCommandHandler(
            IBookRepositoryAsync bookRepository,
            IShopRepositoryAsync shopRepository,
            IShopBookRepositoryAsync shopBookRepositoryAsync,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _shopRepository = shopRepository;
            _shopBookRepositoryAsync = shopBookRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(ImportBookCommand request, CancellationToken cancellationToken)
        {

            var shop = await _shopRepository.GetBookByShopCodeAsync(request.ShopCode);
            if (shop == null) throw new ApiException($"Shop Not Found.");

            foreach (var xmlBook in request.Books)
            {
                var book = await _bookRepository.GetBookByISBNAsync(xmlBook.ISBNCode);

                if (book == null) {
                    book = _mapper.Map<Book>(xmlBook);  
                    await _bookRepository.AddAsync(book);
                }
                
                var shopBook = await _shopBookRepositoryAsync.GetByShopIdAndBookIdAsync(shop.Id, book.Id);
                if (shopBook == null)
                {
                    shopBook = _mapper.Map<ShopBook>(xmlBook);
                    shopBook.ShopId = shop.Id;
                    shopBook.BookId = book.Id;
                    await _shopBookRepositoryAsync.AddAsync(shopBook);
                }
                else
                {
                    shopBook.Price = xmlBook.Price;
                    shopBook.InStock = xmlBook.InStock;
                    await _shopBookRepositoryAsync.AddAsync(shopBook);
                }

            }
            return new Response<int>(request.Books.Count);

        }
    }
}
