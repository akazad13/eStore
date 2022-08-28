using AutoMapper;
using Exino.Application.Common.Interfaces;
using Exino.Application.Common.Utilities;
using Exino.Application.Common.Wrappers;
using Exino.Application.CQRS.User.DTOs;
using MediatR;

namespace Exino.Application.CQRS.User.Commands.UserLogin
{
    public class UserLoginCommand : IRequest<IResult<AuthResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
    public class UserLoginCommandHandler : IRequestHandler<UserLoginCommand, IResult<AuthResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJWTTokenHelper _IJWTTokenHelper;
        private readonly IMapper _mapper;
        public UserLoginCommandHandler(
               IIdentityService identityService,
               IJWTTokenHelper IJWTTokenHelper,
               IMapper mapper
        )
        {
            _identityService = identityService;
            _IJWTTokenHelper = IJWTTokenHelper;
            _mapper = mapper;
        }
        public async Task<IResult<AuthResponse>> Handle(UserLoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _identityService.AuthenticateUser(request.Email, request.Password);

            if (user == null)
            {
                return Response<AuthResponse>.ErrorResponse(new string[] { $"Invalid Credentials for '{request.Email}'." });
            }

            var response = _mapper.Map<AuthResponse>(user);
            response.JWT = await _IJWTTokenHelper.GenerateJwtToken(user);

            return Response<AuthResponse>.SuccessResponese(response);
        }
    }
}
