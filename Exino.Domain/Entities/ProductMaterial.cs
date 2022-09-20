namespace Exino.Domain.Entities
{
    public class ProductMaterial
    {
        public int MaterialId { get; set; }
        public Material? Material { get; set; }
        public int ProductId { get; set; }
        public Product? Product { get; set; }
    }
}
