using AutoMapper;
using Exino.Application.Common.Utilities;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Exino.Application.CQRS.Product.Queries.SearchProduct
{
    public class SearchProductQueryHandler
        : IRequestHandler<SearchProductQueryRequest, IResult<IPaginate<SearchProductQueryResponse>>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        private readonly IHelper _helper;

        public SearchProductQueryHandler(
            IProductRepository productRepository,
            IMapper mapper,
            IHelper helper
        )
        {
            _productRepository = productRepository;
            _mapper = mapper;
            _helper = helper;
        }

        public async Task<IResult<IPaginate<SearchProductQueryResponse>>> Handle(
            SearchProductQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await _productRepository.GetFilteredList(
                selector: x =>
                    new SearchProductQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Price = x.Price,
                        Size = x.Size,
                        Color = x.Color,
                        ThumbnailImagePath =
                            x.ProductImages == null
                                ? null
                                : (
                                    x.ProductImages.Count(p => p.IsThumbnail == true) > 0
                                        ? null
                                        : x.ProductImages
                                            .First(p => p.IsThumbnail == true)
                                            .ImagePath
                                ),
                        Stock = x.Stock,
                        CategoryName = x.Category == null ? null : x.Category.Name
                    },
                expression: x =>
                    x.Status != Status.Deactive
                    && (request.Name == null || (x.Name != null && x.Name.Contains(request.Name)))
                    && (
                        request.Categories == null
                        || (_helper.SplitString(request.Categories)).Any(
                            c => c == x.CategoryId.ToString()
                        )
                    )
                    && (
                        request.Materials == null
                        || _helper
                            .SplitString(request.Materials)
                            .Any(m => m == x.MaterialId.ToString())
                    ),
                orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Category),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                disableTracing: true,
                cancellationToken: cancellationToken
            );

            return Response<IPaginate<SearchProductQueryResponse>>.SuccessResponese(products);
        }
    }
}
