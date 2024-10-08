﻿using eStore.Application.CQRS.Authentication.Commands.Signup;
using eStore.Application.CQRS.Authentication.Queries.Login;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStore.API.Controllers
{
    [Route("api/auth")]
    [AllowAnonymous]
    public class AuthController : ApiControllerBase
    {
        [HttpPost("login")]
        public async Task<IActionResult> Login(UserLoginQueryRequest commnad)
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
