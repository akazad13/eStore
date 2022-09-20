using Exino.Domain.Common;

namespace Exino.Domain.Entities
{
    public class Material : BaseAuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public ICollection<ProductMaterial>? ProductMaterials { get; set; }
    }
}
