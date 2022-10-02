using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Product.Queries.SearchProduct
{
    public class SearchProductQueryRequest
        : IRequest<IResult<IPaginate<SearchProductQueryResponse>>>
    {
        public string? Name { get; set; } = null;
        public string? Categories { get; set; } = null;
        public string? Materials { get; set; } = null;
        public string sortBy { get; set; } = "Name";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
