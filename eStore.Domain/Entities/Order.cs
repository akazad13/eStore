using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class Order : BaseAuditableEntity
    {
        public long Id { get; set; }
        public long UserId { get; set; }
        public AppUser? AppUser { get; set; }
        public decimal Amount { get; set; }

        public string? ShippingAddress { get; set; }

        public string? Address { get; set; }

        public ICollection<OrderDetail>? OrderDetails { get; set; }
    }
}
