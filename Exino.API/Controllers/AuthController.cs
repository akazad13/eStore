using Exino.Application.CQRS.User.Commands.UserLogin;
using Exino.Application.CQRS.User.Commands.UserSignup;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginCommand commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
        [HttpPost("signup")]
        public async Task<IActionResult> Signup(UserSignupCommand commnad)
        {
            return (await Mediator.Send(commnad)).Match<IActionResult>(Ok, BadRequest);
        }
    }
}
