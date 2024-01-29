using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryRequest
        : IRequest<IResult<IPaginate<GetAllCategoriesQueryResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
