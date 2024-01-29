namespace eStore.Application.CQRS.Material.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryResponse
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? MaterialImagePath { get; set; }
    }
}
