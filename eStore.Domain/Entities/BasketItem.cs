using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class BasketItem : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long BasketId { get; set; }
        public Basket? Basket { get; set; }
        public int ProductID { get; set; }
        public Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
