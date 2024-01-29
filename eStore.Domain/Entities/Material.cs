using eStore.Domain.Common;

namespace eStore.Domain.Entities
{
    public class Material : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<Product>? Products { get; set; }
        public string? MaterialImagePath { get; set; }
    }
}
