using AutoMapper;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using MediatR;

namespace eStore.Application.CQRS.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler
        : IRequestHandler<UpdateCategoryCommandRequest, IResult<GenericResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<GenericResponse>> Handle(
            UpdateCategoryCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = _mapper.Map<Domain.Entities.Category>(request);

            await _categoryRepository.Create(model);
            var result = await _categoryRepository.Commit(cancellationToken);

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Category successfully updated.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(
                    new[] { "Failed to save the category" }
                );
            }
        }
    }
}
