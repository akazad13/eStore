using AutoMapper;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using MediatR;

namespace eStore.Application.CQRS.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
                : IRequestHandler<CreateCategoryCommandRequest, IResult<GenericResponse>>
    {
        public async Task<IResult<GenericResponse>> Handle(
            CreateCategoryCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = mapper.Map<Domain.Entities.Category>(request);

            await categoryRepository.Create(model);

            var result = await categoryRepository.Commit(cancellationToken);

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Category successfully created.");
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
