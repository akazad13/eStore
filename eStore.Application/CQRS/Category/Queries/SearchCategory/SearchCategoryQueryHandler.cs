using AutoMapper;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.SearchCategory
{
    public class SearchCategoryQueryHandler
        : IRequestHandler<
            SearchCategoryQueryRequest,
            IResult<IPaginate<SearchCategoryQueryResponse>>
        >
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public SearchCategoryQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<IPaginate<SearchCategoryQueryResponse>>> Handle(
            SearchCategoryQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await _categoryRepository.GetFilteredList(
                selector: x => new SearchCategoryQueryResponse { Id = x.Id, Name = x.Name, },
                expression: x =>
                    x.Status != Status.Deactive
                    && (request.Name == null || (x.Name != null && x.Name.Contains(request.Name))),
                orderBy: x => x.OrderBy(x => x.Name),
                include: null,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                disableTracing: true,
                cancellationToken: cancellationToken
            );

            return Response<IPaginate<SearchCategoryQueryResponse>>.SuccessResponese(products);
        }
    }
}
