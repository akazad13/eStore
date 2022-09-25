using Exino.Domain.Entities;

namespace Exino.Application.CQRS.Product.Queries.GetProductById
{
    public class GetProductByIdQueryResponse
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
        public int? TotalComments { get; set; }
        public List<ProductComment>? ProductComments { get; set; }
        public decimal? Rating { get; set; }
    }
}
