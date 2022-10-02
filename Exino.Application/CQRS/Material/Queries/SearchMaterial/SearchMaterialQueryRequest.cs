﻿using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Material.Queries.SearchMaterial
{
    public class SearchMaterialQueryRequest
        : IRequest<IResult<IPaginate<SearchMaterialQueryResponse>>>
    {
        public string? Name { get; set; } = null;
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }
}
