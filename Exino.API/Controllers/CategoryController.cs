using Exino.Application.CQRS.Category.Commands.CreateCategory;
using Exino.Application.CQRS.Category.Commands.UpdateCategory;
using Exino.Application.CQRS.Category.Queries.GetAllCategories;
using Exino.Application.CQRS.Category.Queries.GetCategoryById;
using Exino.Application.CQRS.Category.Queries.SearchCategory;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllCategoriesQueryRequest request)
        {
            return (await Mediator.Send(request)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return (
                await Mediator.Send(new GetCategoryByIdQueryRequest() { Id = id })
            ).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpGet("search")]
        public async Task<IActionResult> Search([FromQuery] SearchCategoryQueryRequest request)
        {
            return (await Mediator.Send(request)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }

        [HttpPatch]
        public async Task<IActionResult> Update(UpdateCategoryCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
