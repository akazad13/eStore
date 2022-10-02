using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Material.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryRequest : IRequest<IResult<GetMaterialByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
