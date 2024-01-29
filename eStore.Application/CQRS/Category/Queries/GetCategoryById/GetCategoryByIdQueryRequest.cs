using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryRequest : IRequest<IResult<GetCategoryByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
