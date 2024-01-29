using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Material.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryRequest : IRequest<IResult<GetMaterialByIdQueryResponse>>
    {
        public int Id { get; set; }
    }
}
