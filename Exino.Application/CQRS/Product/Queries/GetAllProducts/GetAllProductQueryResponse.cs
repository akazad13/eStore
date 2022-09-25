namespace Exino.Application.CQRS.Product.Queries.GetAllProducts
{
    public class GetAllProductQueryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public IEnumerable<string?>? ImagePaths { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }
        public DateTimeOffset CreatedOn { get; set; }
        public string? CreatedBy { get; set; }
        public DateTimeOffset? ModifiedOn { get; set; }
        public string? ModifiedBy { get; set; }
    }
}
