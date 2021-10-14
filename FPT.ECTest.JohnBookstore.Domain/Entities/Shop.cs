using FPT.ECTest.JohnBookstore.Domain.Common;
using System.Collections.Generic;

namespace FPT.ECTest.JohnBookstore.Domain.Entities
{
    public class Shop : AuditableBaseEntity
    {
        public string ShopCode { get; set; }
        public string ShopName { get; set; }
        public string Description { get; set; }
        public string Email { get; set; }
        public string XMLTemplate { get; set; }
        public bool AllowUpdateViaEmail { get; set; }
        public List<ShopBook> ShopBooks { get; set; }
        public List<Order> Orders { get; set; }
    }

    public class ShopBook : AuditableBaseEntity
    {
        public decimal Price { get; set; }
        public int InStock { get; set; }
        public int BookId { get; set; }
        public Book Book { get; set; }
        public int ShopId { get; set; }
        public Shop Shop { get; set; }

        public string GetShopName()
        {
            return Shop == null ? null : Shop.ShopName;
        }
    }
}
