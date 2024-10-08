using eStore.Application.CQRS.Material.Commands.CreateMaterial;
using eStore.Application.CQRS.Material.Commands.UpdateMaterial;
using eStore.Application.CQRS.Material.Queries.GetMaterialById;
using eStore.Application.CQRS.Material.Queries.SearchMaterial;
using Microsoft.AspNetCore.Mvc;

namespace eStore.API.Controllers
{
    [Route("api/material")]
    public class MaterialController : ApiControllerBase
    {
        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchMaterialQueryRequest request)
        {
            return (await Mediator.Send(request)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return (
                await Mediator.Send(new GetMaterialByIdQueryRequest() { Id = id })
            ).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPost]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> Create([FromForm] CreateMaterialCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPatch]
        [RequestSizeLimit(long.MaxValue)]
        public async Task<IActionResult> Update([FromForm] UpdateMaterialCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
