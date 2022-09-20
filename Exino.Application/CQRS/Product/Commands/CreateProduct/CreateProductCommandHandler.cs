using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Product.Commands.CreateProduct
{
    //public class CreateProductCommandHandler : IRequestHandler<CreateProductCommandRequest, GenericResponse>
    //{
    //    private readonly IProductRepository _productRepository;
    //    private readonly IMapper _mapper;

    //    public CreateProductCommandHandler(IProductRepository productRepository, IMapper mapper)
    //    {
    //        _productRepository = productRepository;
    //        _mapper = mapper;
    //    }


    //    public async Task<CreateProductCommandResponse> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
    //    {

    //        var model = _mapper.Map<ECommerceApi.Domain.Entities.Product>(request);

    //        await _productRepository.Create(model);

    //        return new CreateProductCommandResponse
    //        {
    //            IsSuccess = true,
    //            ProductId = model.Id
    //        };
    //    }
    //}
}
