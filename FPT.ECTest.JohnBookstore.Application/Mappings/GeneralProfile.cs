using AutoMapper;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Commands.CreateBook;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks;
using FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetBookByISBN;
using FPT.ECTest.JohnBookstore.Domain.Entities;

namespace FPT.ECTest.JohnBookstore.Application.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {

            CreateMap<Book, GetAllBooksViewModel>();
            CreateMap<CreateBookCommand, Book>();
            CreateMap<GetAllBooksQuery, GetAllBooksParameter>();
            CreateMap<FindBooksQuery, FindBooksParameter>();
            CreateMap<Book, GetBookByISBNViewModel>();
            CreateMap<ShopBook, ShopBookViewModel>();
            CreateMap<OrderBookCommand, Order>();

        }
    }
}
