using AutoMapper;
using eStore.Application.Common.Wrappers;
using eStore.Application.CQRS.Product.Dtos;
using eStore.Application.RepositoriesInterface;
using eStore.Domain.Enums;
using MediatR;

namespace eStore.Application.CQRS.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler(IProductRepository productRepository)
                : IRequestHandler<GetProductByIdQueryRequest, IResult<GetProductByIdQueryResponse>>
    {
        public async Task<IResult<GetProductByIdQueryResponse>> Handle(
            GetProductByIdQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var product = await productRepository.GetFilteredFirstOrDefault(
                selector: x =>
                    new GetProductByIdQueryResponse
                    {
                        Id = x.Id,
                        Name = x.Name,
                        Description = x.Description,
                        Price = x.Price,
                        Size = x.Size,
                        Color = x.Color,
                        ImagePaths =
                            x.ProductImages == null
                                ? null
                                : x.ProductImages.Select(p => p.ImagePath),
                        Stock = x.Stock,
                        CategoryName = x.Category == null ? null : x.Category.Name,
                        Rating =
                            x.ProductRatings == null
                                ? null
                                : x.ProductRatings.Average(y => y.Rating),
                        TotalComments =
                            x.ProductComments == null
                                ? null
                                : x.ProductComments.Count(y => y.Id == x.Id),
                        ProductComments =
                            x.ProductComments == null
                                ? null
                                : x.ProductComments
                                    .Where(
                                        x =>
                                            x.ProductId == request.Id && x.Status != Status.Deactive
                                    )
                                    .OrderByDescending(x => x.CreatedOn)
                                    .Select(
                                        x =>
                                            new ProductCommentDto
                                            {
                                                Id = x.Id,
                                                UserId = x.UserId,
                                                Text = x.Text,
                                                UserImage =
                                                    x.AppUser == null ? null : x.AppUser.ImagePath,
                                                UserName =
                                                    x.AppUser == null ? null : x.AppUser.UserName,
                                                CreateDate = x.CreatedOn.ToString()
                                            }
                                    )
                                    .ToList(),
                    },
                expression: x => x.Id == request.Id && x.Status != Status.Deactive
            );
            return Response<GetProductByIdQueryResponse>.SuccessResponese(product?? new GetProductByIdQueryResponse());
        }
    }
}
