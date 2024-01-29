using AutoMapper;
using eStore.Application.Common.Services.AWSS3;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using MediatR;

namespace eStore.Application.CQRS.Material.Commands.CreateMaterial
{
    public class CreateMaterialCommandHandler
        : IRequestHandler<CreateMaterialCommandRequest, IResult<GenericResponse>>
    {
        private readonly IMaterialRepository _materialRepository;
        private readonly IMapper _mapper;
        private readonly IAWSS3Service _AWSS3Service;

        public CreateMaterialCommandHandler(
            IMaterialRepository materialRepository,
            IMapper mapper,
            IAWSS3Service AWSS3Service
        )
        {
            _materialRepository = materialRepository;
            _mapper = mapper;
            _AWSS3Service = AWSS3Service;
        }

        public async Task<IResult<GenericResponse>> Handle(
            CreateMaterialCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var model = _mapper.Map<Domain.Entities.Material>(request);

                if (request.MaterialImage != null)
                {
                    var filename = $"mat_{Guid.NewGuid().ToString()}";
                    using (var newMemoryStream = new MemoryStream())
                    {
                        request.MaterialImage.CopyTo(newMemoryStream);
                        var isUploaded = await _AWSS3Service.PushToAmazonS3ViaRest(
                            filename,
                            newMemoryStream
                        );
                        if (isUploaded)
                        {
                            model.MaterialImagePath = filename;
                        }
                    }
                }

                await _materialRepository.Create(model);
                var result = await _materialRepository.Commit(cancellationToken);

                if (result)
                {
                    return Response<GenericResponse>.SuccessResponese(
                        "Material successfully created."
                    );
                }
                else
                {
                    return Response<GenericResponse>.ErrorResponse(
                        new[] { "Failed to save the material" }
                    );
                }
            }
            catch (Exception ex)
            {
                return Response<GenericResponse>.ErrorResponse(
                    new[] { "Failed to save the material" }
                );
            }
        }
    }
}
