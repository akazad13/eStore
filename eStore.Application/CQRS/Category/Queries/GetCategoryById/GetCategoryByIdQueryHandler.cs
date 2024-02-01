using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Category.Queries.GetCategoryById
{
    public class GetCategoryByIdQueryHandler(ICategoryRepository categoryRepository)
                : IRequestHandler<GetCategoryByIdQueryRequest, IResult<GetCategoryByIdQueryResponse>>
    {
        public async Task<IResult<GetCategoryByIdQueryResponse>> Handle(
            GetCategoryByIdQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var category = await categoryRepository.GetFilteredFirstOrDefault(
                selector: x => new GetCategoryByIdQueryResponse { Id = x.Id, Name = x.Name, },
                expression: x => x.Id == request.Id && x.Status != Status.Deactive
            );

            return Response<GetCategoryByIdQueryResponse>.SuccessResponese(category?? new GetCategoryByIdQueryResponse());
        }
    }
}
