using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exino.Application.CQRS.Product.Queries.GetAllProducts
{
    public class GetAllProductQueryHandler : IRequestHandler<GetAllProductQueryRequest, IResult<List<GetAllProductQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetAllProductQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<IResult<List<GetAllProductQueryResponse>>> Handle(GetAllProductQueryRequest request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetFilteredList(
                selector: x => new GetAllProductQueryResponse
                {

                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Size = x.Size,
                    Color = x.Color,
                    ImagePaths = x.ProductImages == null ? null : x.ProductImages.Select(p => p.ImagePath),
                    Stock = x.Stock,
                    CategoryName = x.Category == null ? null : x.Category.Name

                },
                expression: x => x.Status != Status.Deactive,
                orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Category),
                pageIndex: 1,
                pageSize: 10
                );

            return Response<List<GetAllProductQueryResponse>>.SuccessResponese(products);
        }
    }
}
