using eStore.Application.CQRS.Users.Commands.UpdateUser;
using Microsoft.AspNetCore.Mvc;

namespace eStore.API.Controllers
{
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {
        [HttpPatch("{userid}")]
        public async Task<IActionResult> Login(int userId, UpdateUserCommand commnad)
        {
            commnad.UserId = userId;
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
