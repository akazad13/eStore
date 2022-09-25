using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Product.Queries.GetProductById
{
    public class GetProductByIdQueryRequest : IRequest<IResult<GetProductByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
