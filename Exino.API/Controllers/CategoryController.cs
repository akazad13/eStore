using Exino.Application.CQRS.Category.Commands.CreateProduct;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class CategoryController : ApiControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(CreateCategoryCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
