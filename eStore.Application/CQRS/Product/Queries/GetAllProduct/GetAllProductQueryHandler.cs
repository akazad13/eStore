using eStore.Application.Common.Wrappers;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace eStore.Application.CQRS.Product.Queries.GetAllProduct
{
    public class GetAllProductQueryHandler(IProductRepository productRepository)
                : IRequestHandler<GetAllProductQueryRequest, IResult<IPaginate<GetAllProductQueryResponse>>>
    {
        public async Task<IResult<IPaginate<GetAllProductQueryResponse>>> Handle(
            GetAllProductQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var products = await productRepository.GetFilteredList(
                selector: x =>
                    new GetAllProductQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Size = x.Size,
                        Color = x.Color,
                        ThumbnailImagePath =
                            x.ProductImages == null
                                ? null
                                : (
                                    x.ProductImages!.Any(p => p.IsThumbnail == true) ?
                                        x.ProductImages!
                                            .First(p => p.IsThumbnail == true)
                                            !.ImagePath : null
                                ),
                        Stock = x.Stock,
                        CategoryName = x.Category == null ? null : x.Category.Name
                    },
                expression: x => x.Status != Status.Deactive,
                orderBy: x => x.OrderBy(x => x.Name),
                include: x => x.Include(x => x.Category),
                pageNumber: request.PageNumber,
                pageSize: request.PageSize,
                disableTracing: true,
                cancellationToken: cancellationToken
            );

            return Response<IPaginate<GetAllProductQueryResponse>>.SuccessResponese(products);
        }
    }
}
