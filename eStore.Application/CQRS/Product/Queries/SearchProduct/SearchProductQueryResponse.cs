namespace eStore.Application.CQRS.Product.Queries.SearchProduct
{
    public class SearchProductQueryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? ThumbnailImagePath { get; set; }
        public int Stock { get; set; }
        public string? CategoryName { get; set; }
    }
}
