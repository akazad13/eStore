using Exino.Application.CQRS.AppUser.Commands;
using Microsoft.AspNetCore.Mvc;

namespace Exino.API.Controllers
{
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginCommand commnad)
        {
            try
            {
                var auth = await Mediator.Send(commnad);
                return Ok(auth);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
