using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Exceptions;
using FPT.ECTest.JohnBookstore.Application.Interfaces.Repositories;
using FPT.ECTest.JohnBookstore.Application.Wrappers;
using FPT.ECTest.JohnBookstore.Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations;
using System.Threading;
using System.Threading.Tasks;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook
{
    public partial class OrderBookCommand : IRequest<Response<int>>
    {
        public string ISBNCode { get; set; }
        public string ShopCode { get; set; }

        [EmailAddress]
        public string Email { get; set; }
    }
    public class OrderBookCommandHandler : IRequestHandler<OrderBookCommand, Response<int>>
    {
        private readonly IBookRepositoryAsync _bookRepository;
        private readonly IOrderRepositoryAsync _orderRepository;
        private readonly IShopRepositoryAsync _shopRepository;
        private readonly IShopBookRepositoryAsync _shopBookRepositoryAsync;
        private readonly IMapper _mapper;
        public OrderBookCommandHandler(
            IBookRepositoryAsync bookRepository, 
            IOrderRepositoryAsync orderRepository,
            IShopRepositoryAsync shopRepository,
            IShopBookRepositoryAsync shopBookRepositoryAsync,
            IMapper mapper)
        {
            _bookRepository = bookRepository;
            _orderRepository = orderRepository;
            _shopRepository = shopRepository;
            _shopBookRepositoryAsync = shopBookRepositoryAsync;
            _mapper = mapper;
        }

        public async Task<Response<int>> Handle(OrderBookCommand request, CancellationToken cancellationToken)
        {
            var order = _mapper.Map<Order>(request);

            var book = await _bookRepository.GetBookByISBNAsync(order.ISBNCode);
            if (book == null) throw new ApiException($"Book Not Found.");

            var shop = await _shopRepository.GetBookByShopCodeAsync(order.ShopCode);
            if (shop == null) throw new ApiException($"Shop Not Found.");

            var shopBook = await _shopBookRepositoryAsync.GetByShopIdAndBookIdAsync(shop.Id, book.Id);
            if (shopBook == null || shopBook.InStock < 1)
                throw new ApiException($"Book out of stock.");

            order.BookId = book.Id;
            order.ShopId = shop.Id;

            await _orderRepository.AddAsync(order);
            shopBook.InStock--;
            await _shopBookRepositoryAsync.UpdateAsync(shopBook);

            return new Response<int>(order.Id);
        }
    }
}
