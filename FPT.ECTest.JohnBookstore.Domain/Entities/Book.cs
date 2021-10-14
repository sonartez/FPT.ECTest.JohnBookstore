using FPT.ECTest.JohnBookstore.Domain.Common;
using System.Collections.Generic;
using System.Linq;

namespace FPT.ECTest.JohnBookstore.Domain.Entities
{
    public class Book : AuditableBaseEntity
    {

        public string ISBNCode { get; set; }
        public string BookName { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public virtual List<ShopBook> ShopBooks { get; set; }
        public virtual List<Order> Orders { get; set; }

        public int GetTotalInStock()
        {
            return ShopBooks == null ? 0 : ShopBooks.Sum(x => x.InStock);
        }
        public decimal GetMinPrice()
        {
            return ShopBooks== null ? 0 : ShopBooks.Min(x => x.Price);
        }
        public decimal GetMaxPrice()
        {
            return ShopBooks == null ? 0 : ShopBooks.Max(x => x.Price);
        }
    }

}
