using Exino.Application.CQRS.Category.Commands.CreateCategory;
using Exino.Application.CQRS.Category.Queries.GetAllCategories;
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

        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
