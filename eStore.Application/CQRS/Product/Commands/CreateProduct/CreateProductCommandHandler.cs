using AutoMapper;
using eStore.Application.Common.Services.AWSS3;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Entities;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler(
        IProductRepository productRepository,
        IMapper mapper,
        IAwsS3Service AWSS3Service,
        IProductImageRepository productImageRepository
        )
                : IRequestHandler<CreateProductCommandRequest, IResult<GenericResponse>>
    {
        private readonly IAwsS3Service _AWSS3Service = AWSS3Service;

        public async Task<IResult<GenericResponse>> Handle(
            CreateProductCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var model = mapper.Map<Domain.Entities.Product>(request);
            model.Status = Status.Active;
            await productRepository.Create(model);

            var result = await productRepository.Commit(cancellationToken);

            List<ProductImage> images = new();
            if (request.Images?.Count > 0)
            {
                foreach (var image in request.Images)
                {
                    var filename = $"product_{Guid.NewGuid().ToString()}";
                    using var newMemoryStream = new MemoryStream();
                    image.CopyTo(newMemoryStream);
                    var isUploaded = await _AWSS3Service.PushToAmazonS3ViaRest(
                        filename,
                        newMemoryStream
                    );
                    if (isUploaded)
                    {
                        images.Add(
                            new ProductImage()
                            {
                                ImagePath = filename,
                                ProductId = model.Id,
                                Status = Status.Active
                            }
                        );
                    }
                }

                await productImageRepository.Create(images);
                _ = await productImageRepository.Commit(cancellationToken);
            }

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Product successfully created.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(
                    ["Failed to save the product"]
                );
            }
        }
    }
}
