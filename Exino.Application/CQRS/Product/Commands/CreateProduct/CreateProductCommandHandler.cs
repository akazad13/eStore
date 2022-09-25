using AutoMapper;
using Exino.Application.Common.Services.AWSS3;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Entities;
using Exino.Domain.Enums;
using MediatR;

namespace Exino.Application.CQRS.Product.Commands.CreateProduct
{
    public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, IResult<GenericResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IAWSS3Service _AWSS3Service;
        private readonly IProductImageRepository _productImageRepository;

        public CreateProductCommandHandler(
            IProductRepository productRepository, 
            IMapper mapper, 
            IAWSS3Service AWSS3Service, 
            IProductImageRepository productImageRepository
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _AWSS3Service = AWSS3Service;
            _productImageRepository = productImageRepository;
        }


        public async Task<IResult<GenericResponse>> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {

            var model = _mapper.Map<Domain.Entities.Product>(request);
            model.Status = Status.Active;
            await _productRepository.Create(model);

            var result = await _productRepository.Commit(cancellationToken);

            List<ProductImage> images = new();
            if (request.Images?.Count > 0)
            {
                foreach (var image in request.Images)
                {
                    var filename = model.Name + Guid.NewGuid().ToString();
                    using (var newMemoryStream = new MemoryStream())
                    {
                        image.CopyTo(newMemoryStream);
                        var isUploaded = await _AWSS3Service.PushToAmazonS3ViaRest(filename, newMemoryStream);
                        if(isUploaded)
                        {
                            images.Add(new ProductImage() {
                                ImagePath = filename,
                                ProductId = model.Id,
                                Status = Status.Active
                            });
                        }
                    }
                }

                await _productImageRepository.Create(images);
                var iamgeResult = await _productImageRepository.Commit(cancellationToken);
            }

            if (result)
            {
                return Response<GenericResponse>.SuccessResponese("Product successfully created.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(new[] { "Failed to save the product" });
            }
        }
    }
}
