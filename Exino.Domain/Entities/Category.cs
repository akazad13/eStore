using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class Category : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
    }
}
