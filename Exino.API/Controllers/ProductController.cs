using Exino.Application.CQRS.Product.Commands.CreateProduct;
using Exino.Application.CQRS.Product.Queries.GetAllProducts;
using Exino.Application.CQRS.Product.Queries.GetProductById;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class ProductController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return (await Mediator.Send(new GetAllProductQueryRequest())).Match<IActionResult>(Ok, BadRequest);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return (await Mediator.Send(new GetProductByIdQueryRequest() { Id = id })).Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateProductCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
