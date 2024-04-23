using eStore.Application.Common.Interfaces;
using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Users.Commands.UpdateUser;

public class UpdateUserCommandHandler(
        IIdentityService identityService
        )
                : IRequestHandler<UpdateUserCommand, IResult<GenericResponse>>
    {
        private readonly IIdentityService _identityService = identityService;

        public async Task<IResult<GenericResponse>> Handle(
            UpdateUserCommand request,
            CancellationToken cancellationToken
        )
        {
            var user = await _identityService.GetUserAsync(request.UserId);
            if (user is null)
            {
                return Response<GenericResponse>.ErrorResponse(
                    [$"Unable to find the User!."]
                );
            }

            user.Locale = request.Locale;

            var result = await _identityService.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                return Response<GenericResponse>.SuccessResponese("Update successful.");
            }
            else
            {
                return Response<GenericResponse>.ErrorResponse(result.Errors);
            }
        }
    }