using Exino.Application.CQRS.Authentication.Commands.Signup;
using Exino.Application.CQRS.Authentication.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    [AllowAnonymous]
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
