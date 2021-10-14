namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetAllBooks
{
    public class GetAllBooksViewModel
    {
        public int Id { get; set; }
        public string ISBNCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int TotalInStock { get; set; }
    }
}
