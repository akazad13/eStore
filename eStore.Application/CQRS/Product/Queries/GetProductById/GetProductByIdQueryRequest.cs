using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Product.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<IResult<GetProductByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
