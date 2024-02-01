using AutoMapper;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using MediatR;

namespace eStore.Application.CQRS.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
                : IRequestHandler<UpdateCategoryCommandRequest, IResult<GenericResponse>>
    {
        public async Task<IResult<GenericResponse>> Handle(
            UpdateCategoryCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = mapper.Map<Domain.Entities.Category>(request);

            await categoryRepository.Create(model);
            var result = await categoryRepository.Commit(cancellationToken);

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Category successfully updated.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(
                    ["Failed to save the category"]
                );
            }
        }
    }
}
