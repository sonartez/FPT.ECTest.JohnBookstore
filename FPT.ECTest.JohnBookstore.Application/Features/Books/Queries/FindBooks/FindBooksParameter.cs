using FPT.ECTest.JohnBookstore.Application.Filters;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks
{
    public class FindBooksParameter : RequestParameter
    {
        public string FindText { get; set; }
    }
}
