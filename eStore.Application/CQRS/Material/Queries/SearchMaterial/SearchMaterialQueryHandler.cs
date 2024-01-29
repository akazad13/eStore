using AutoMapper;
using eStore.Application.Common.Utilities;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Material.Queries.SearchMaterial
{
    public class SearchMaterialQueryHandler
        : IRequestHandler<
            SearchMaterialQueryRequest,
            IResult<IPaginate<SearchMaterialQueryResponse>>
        >
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public SearchMaterialQueryHandler(
            IMaterialRepository materialRepository,
            IMapper mapper,
            IHelper helper
        )
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<IResult<IPaginate<SearchMaterialQueryResponse>>> Handle(
            SearchMaterialQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await _materialRepository.GetFilteredList(
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
