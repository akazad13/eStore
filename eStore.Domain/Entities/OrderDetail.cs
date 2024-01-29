using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class OrderDetail : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public Order? Order { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
