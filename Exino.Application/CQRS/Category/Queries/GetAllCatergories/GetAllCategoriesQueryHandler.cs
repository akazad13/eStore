using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;

namespace Exino.Application.CQRS.Category.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler
        : IRequestHandler<
            GetAllCategoriesQueryRequest,
            IResult<IPaginate<GetAllCategoriesQueryResponse>>
        >
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<IPaginate<GetAllCategoriesQueryResponse>>> Handle(
            GetAllCategoriesQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await _categoryRepository.GetFilteredList(
                selector: x => new GetAllCategoriesQueryResponse { Id = x.Id, Name = x.Name, },
                expression: x => x.Status != Status.Deactive,
                orderBy: x => x.OrderBy(x => x.Name),
                include: null,
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                disableTracing: true,
                cancellationToken: cancellationToken
            );

            return Response<IPaginate<GetAllCategoriesQueryResponse>>.SuccessResponese(products);
        }
    }
}
