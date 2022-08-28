using Exino.Application.Common.Interfaces;
using Exino.Application.Common.Utilities;
using Exino.Application.Common.Wrappers;
using Exino.Application.CQRS.AppUser.DTOs;
using MediatR;

namespace Exino.Application.CQRS.AppUser.Commands
{
    public class UserLoginCommand : IRequest<IResult<AuthResponse>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, IResult<AuthResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJWTTokenHelper _IJWTTokenHelper;
        public UserLoginCommandHandler(
               IIdentityService identityService,
               IJWTTokenHelper IJWTTokenHelper
        )
        {
            _identityService = identityService;
            _IJWTTokenHelper = IJWTTokenHelper;
        }
        public async Task<IResult<AuthResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.AuthenticateUser(request.Email, request.Password);

            if (user == null)
            {
                return Response<AuthResponse>.ErrorResponse(new string[] { $"Invalid Credentials for '{request.Email}'." });
            }

            var response = new AuthResponse
            {
                FirstName = user.FirstName,
                LastName = user.LastName,
                JWT = await _IJWTTokenHelper.GenerateJwtToken(user)
            };

            return Response<AuthResponse>.SuccessResponese(response);
        }
    }
}
