﻿using Exino.Application.Common.Interfaces;
using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Authentication.Commands.Signup
{
    public class UserSignupCommandHandler
        : IRequestHandler<UserSignupCommandRequest, IResult<GenericResponse>>
    {
        private readonly IIdentityService _identityService;

        public UserSignupCommandHandler(IIdentityService identityService)
        {
            _identityService = identityService;
        }

        public async Task<IResult<GenericResponse>> Handle(
            UserSignupCommandRequest request,
            CancellationToken cancellationToken
        )
        {
            var response = await _identityService.IsUserExist(request.Email);
            if (response == true)
            {
                return Response<GenericResponse>.ErrorResponse(
                    new string[] { $"There is already an user registered with this email!." }
                );
            }

            var result = await _identityService.CreateUserAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.IsSubscribeToNewsletter,
                new[] { "Customer" }
            );

            if (result.Result.Succeeded)
            {
                return Response<GenericResponse>.SuccessResponese("Signup successful.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(result.Result.Errors);
            }
        }
    }
}