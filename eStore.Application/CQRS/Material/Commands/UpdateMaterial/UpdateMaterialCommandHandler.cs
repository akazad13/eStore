using AutoMapper;
using eStore.Application.Common.Services.AWSS3;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using MediatR;

namespace eStore.Application.CQRS.Material.Commands.UpdateMaterial
{
    public class UpdateMaterialCommandHandler(
        IMaterialRepository materialRepository,
        IMapper mapper,
        IAwsS3Service AWSS3Service
        )
                : IRequestHandler<UpdateMaterialCommandRequest, IResult<GenericResponse>>
    {
        private readonly IAwsS3Service _AWSS3Service = AWSS3Service;

        public async Task<IResult<GenericResponse>> Handle(
            UpdateMaterialCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            try
            {
                var model = mapper.Map<Domain.Entities.Material>(request);

                if (request.MaterialImage != null)
                {
                    var filename = $"mat_{Guid.NewGuid().ToString()}";
                    using var newMemoryStream = new MemoryStream();
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

                materialRepository.Update(model);
                var result = await materialRepository.Commit(cancellationToken);

                if (result)
                {
                    return Response<GenericResponse>.SuccessResponese(
                        "Material successfully updated."
                    );
                }
                else
                {
                    return Response<GenericResponse>.ErrorResponse(
                        ["Failed to save the material"]
                    );
                }
            }
            catch (Exception)
            {
                return Response<GenericResponse>.ErrorResponse(
                    ["Failed to save the material"]
                );
            }
        }
    }
}
