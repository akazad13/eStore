using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;

namespace Exino.Application.CQRS.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler
        : IRequestHandler<CreateCategoryCommandRequest, IResult<GenericResponse>>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<IResult<GenericResponse>> Handle(
            CreateCategoryCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = _mapper.Map<Domain.Entities.Category>(request);

            await _categoryRepository.Create(model);

            var result = await _categoryRepository.Commit(cancellationToken);

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Category successfully created.");
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
