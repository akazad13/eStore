﻿using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Product.Queries.GetAllProduct
{
    public class GetAllProductQueryRequest
        : IRequest<IResult<IPaginate<GetAllProductQueryResponse>>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
