﻿using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Product.Queries.SearchProduct
{
    public class SearchProductQueryRequest
        : IRequest<IResult<IPaginate<SearchProductQueryResponse>>>
    {
        public string? Name { get; set; } = null;
        public string? Categories { get; set; } = null;
        public string? Materials { get; set; } = null;
        public string SortBy { get; set; } = "Name";
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
