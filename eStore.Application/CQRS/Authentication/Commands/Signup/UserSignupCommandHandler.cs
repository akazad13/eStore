using eStore.Application.Common.Interfaces;
using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Authentication.Commands.Signup;
    public class UserSignupCommandHandler(IIdentityService identityService)
                : IRequestHandler<UserSignupCommand, IResult<GenericResponse>>
    {
        public async Task<IResult<GenericResponse>> Handle(
            UserSignupCommand request,
            CancellationToken cancellationToken
        )
        {
            var response = await identityService.IsUserExist(request.Email);
            if (response)
            {
                return Response<GenericResponse>.ErrorResponse(
                    [$"There is already an user registered with this email!."]
                );
            }

            var result = await identityService.CreateUserAsync(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Password,
                request.IsSubscribeToNewsletter,
                [ "Customer" ]
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
