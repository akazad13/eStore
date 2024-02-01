using eStore.Application.Common.Utilities;
using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eStore.Application.CQRS.Product.Queries.SearchProduct
{
    public class SearchProductQueryHandler(IProductRepository productRepository, IHelper helper)
                : IRequestHandler<SearchProductQueryRequest, IResult<IPaginate<SearchProductQueryResponse>>>
    {
        public async Task<IResult<IPaginate<SearchProductQueryResponse>>> Handle(
            SearchProductQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await productRepository.GetFilteredList(
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
                                    x.ProductImages.Any(p => p.IsThumbnail == true) ?
                                        x.ProductImages
                                            .First(p => p.IsThumbnail == true)
                                            .ImagePath : ""
                                ),
                        Stock = x.Stock,
                        CategoryName = x.Category == null ? null : x.Category.Name
                    },
                expression: x =>
                    x.Status != Status.Deactive
                    && (request.Name == null || (x.Name != null && x.Name.Contains(request.Name)))
                    && (
                        request.Categories == null
                        || (helper.SplitString(request.Categories)).Any(
                            c => c == x.CategoryId.ToString()
                        )
                    )
                    && (
                        request.Materials == null
                        || helper
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
