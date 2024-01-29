using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Category.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryRequest : IRequest<IResult<GetCategoryByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
