using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Product.Queries.GetAllProducts
{
    public class GetAllProductQueryRequest : IRequest<IResult<List<GetAllProductQueryResponse>>>
    {
    }
}
