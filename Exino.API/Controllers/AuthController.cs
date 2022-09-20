using Exino.Application.CQRS.User.Commands.Signup;
using Exino.Application.CQRS.User.Queries.Login;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginQueryRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserSignupCommandRequest commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
