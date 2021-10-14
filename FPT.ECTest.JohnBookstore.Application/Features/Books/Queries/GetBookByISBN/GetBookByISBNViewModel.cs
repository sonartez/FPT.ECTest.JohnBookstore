using System.Collections.Generic;

namespace FPT.ECTest.JohnBookstore.Application.Features.Books.Queries.GetBookByISBN
{
    public class GetBookByISBNViewModel
    {
        public string ISBNCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPrice { get; set; }
        public int TotalInStock { get; set; }

        public List<ShopBookViewModel> ShopBooks { get; set; }

    }

    public class ShopBookViewModel 
    {
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public string ShopName { get; set; }
    }
}
