﻿using eStore.Application.CQRS.Product.Commands.CreateProduct;
using eStore.Application.CQRS.Product.Queries.GetAllProduct;
using eStore.Application.CQRS.Product.Queries.GetProductById;
using eStore.Application.CQRS.Product.Queries.SearchProduct;
using Microsoft.AspNetCore.Mvc;

namespace eStore.API.Controllers
{
    [Route("api/product")]
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllProductQueryRequest request)
        {
            return (await Mediator.Send(request)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return (
                await Mediator.Send(new GetProductByIdQueryRequest() { Id = id })
            ).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchProductQueryRequest request)
        {
            return (await Mediator.Send(request)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> Create([FromForm] CreateProductCommand commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
