using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.GetAllCategories
{
    public class GetAllCategoriesQueryHandler(ICategoryRepository categoryRepository)
                : IRequestHandler<
            GetAllCategoriesQueryRequest,
            IResult<IPaginate<GetAllCategoriesQueryResponse>>
        >
    {
        public async Task<IResult<IPaginate<GetAllCategoriesQueryResponse>>> Handle(
            GetAllCategoriesQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await categoryRepository.GetFilteredList(
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
