using AutoMapper;
using Exino.Application.Common.Authentication;
using Exino.Application.Common.Interfaces;
using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryHandler
        : IRequestHandler<UserLoginQueryRequest, IResult<UserLoginQueryResponse>>
    {
        private readonly IIdentityService _identityService;
        private readonly IJWTTokenGenerator _IJWTTokenGenerator;
        private readonly IMapper _mapper;

        public UserLoginQueryHandler(
            IIdentityService identityService,
            IJWTTokenGenerator IJWTTokenGenerator,
            IMapper mapper
        )
        {
            _identityService = identityService;
            _IJWTTokenGenerator = IJWTTokenGenerator;
            _mapper = mapper;
        }

        public async Task<IResult<UserLoginQueryResponse>> Handle(
            UserLoginQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await _identityService.AuthenticateUser(request.Email, request.Password);

            if (user == null)
            {
                return Response<UserLoginQueryResponse>.ErrorResponse(
                    new string[] { $"Invalid Credentials for '{request.Email}'." }
                );
            }

            var response = _mapper.Map<UserLoginQueryResponse>(user);
            response.JWT = await _IJWTTokenGenerator.GenerateJwtToken(user);

            return Response<UserLoginQueryResponse>.SuccessResponese(response);
        }
    }
}
