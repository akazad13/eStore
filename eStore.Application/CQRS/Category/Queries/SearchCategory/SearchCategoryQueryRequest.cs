using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.SearchCategory
{
    public class SearchCategoryQueryRequest
        : IRequest<IResult<IPaginate<SearchCategoryQueryResponse>>>
    {
        public string? Name { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
