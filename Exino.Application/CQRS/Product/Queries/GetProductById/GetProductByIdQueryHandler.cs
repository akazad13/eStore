using AutoMapper;
using Exino.Application.Common.Wrappers;
using Exino.Application.RepositoriesInterface;
using Exino.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Exino.Application.CQRS.Product.Queries.GetProductById
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQueryRequest, IResult<GetProductByIdQueryResponse>>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductByIdQueryHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }


        public async Task<IResult<GetProductByIdQueryResponse>> Handle(GetProductByIdQueryRequest request, CancellationToken cancellationToken)
        {

            var products = await _productRepository.GetFilteredFirstOrDefault(
                selector: x => new GetProductByIdQueryResponse
                {

                    Id = x.Id,
                    Name = x.Name,
                    Description = x.Description,
                    Price = x.Price,
                    Size = x.Size,
                    Color = x.Color,
                    ImagePaths = x.ProductImages == null ? null : x.ProductImages.Select(p => p.ImagePath),
                    Stock = x.Stock,
                    CategoryName = x.Category == null ? null : x.Category.Name,
                    Rating = x.ProductRatings == null ? null : x.ProductRatings.Average(y => y.Rating),
                    TotalComments = x.ProductComments == null ? null : x.ProductComments.Count(y => y.Id == x.Id),

                    ProductComments = x.ProductComments == null ? null : x.ProductComments.Where(x => x.ProductId == request.Id && x.Status != Status.Deactive)
                    .OrderByDescending(x => x.CreatedOn).ToList()
                    //.Select(x => new ProductCommentDto
                    //{
                    //    Id = x.Id,
                    //    User_Id = x.User_Id,
                    //    Text = x.Text,
                    //    UserImage = x.User.ImagePath,
                    //    UserName = x.User.UserName,
                    //    CreateDate = x.CreateDate.ToString()
                    //}).ToList(),

                },
                expression: x => x.Id == request.Id &&
                                x.Status != Status.Deactive
                );

            return Response<GetProductByIdQueryResponse>.SuccessResponese(products);
        }
    }
}
