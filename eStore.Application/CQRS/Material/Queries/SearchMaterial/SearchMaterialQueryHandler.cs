using AutoMapper;
using eStore.Application.Common.Utilities;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Material.Queries.SearchMaterial
{
    public class SearchMaterialQueryHandler(
        IMaterialRepository materialRepository
        )
                : IRequestHandler<
            SearchMaterialQueryRequest,
            IResult<IPaginate<SearchMaterialQueryResponse>>
        >
    {
        public async Task<IResult<IPaginate<SearchMaterialQueryResponse>>> Handle(
            SearchMaterialQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await materialRepository.GetFilteredList(
                selector: x =>
                    new SearchMaterialQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        MaterialImagePath = x.MaterialImagePath
                    },
                expression: x =>
                    x.Status != Status.Deactive
                    && (request.Name == null || (x.Name != null && x.Name.Contains(request.Name))),
                orderBy: x => x.OrderBy(x => x.Name),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                disableTracing: true,
                cancellationToken: cancellationToken
            );

            return Response<IPaginate<SearchMaterialQueryResponse>>.SuccessResponese(products);
        }
    }
}
