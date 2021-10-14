using FPT.ECTest.JohnBookstore.Domain.Common;

namespace FPT.ECTest.JohnBookstore.Domain.Entities
{
    public class Order : AuditableBaseEntity
    {
        public string ShopCode { get; set; }
        public string ISBNCode { get; set; }
        public string Email { get; set; }

        public int BookId { get; set; }
        public virtual Book Book { get; set; }
        public int ShopId { get; set; }
        public virtual Shop Shop { get; set; }

    }
}
