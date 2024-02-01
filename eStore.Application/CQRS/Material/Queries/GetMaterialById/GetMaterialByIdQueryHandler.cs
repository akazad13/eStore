using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Material.Queries.GetMaterialById
{
    public class GetMaterialByIdQueryHandler(IMaterialRepository materialRepository)
                : IRequestHandler<GetMaterialByIdQueryRequest, IResult<GetMaterialByIdQueryResponse>>
    {
        public async Task<IResult<GetMaterialByIdQueryResponse>> Handle(
            GetMaterialByIdQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var material = await materialRepository.GetFilteredFirstOrDefault(
                selector: x =>
                    new GetMaterialByIdQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        MaterialImagePath = x.MaterialImagePath,
                    },
                expression: x => x.Id == request.Id && x.Status != Status.Deactive
            );
            return Response<GetMaterialByIdQueryResponse>.SuccessResponese(material?? new GetMaterialByIdQueryResponse());
        }
    }
}
