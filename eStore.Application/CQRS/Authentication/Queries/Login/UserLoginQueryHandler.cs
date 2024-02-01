using AutoMapper;
using eStore.Application.Common.Authentication;
using eStore.Application.Common.Interfaces;
using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryHandler(
        IIdentityService identityService,
        IJwtTokenGenerator IJwtTokenGenerator,
        IMapper mapper
        )
                : IRequestHandler<UserLoginQueryRequest, IResult<UserLoginQueryResponse>>
    {
        private readonly IJwtTokenGenerator _IJwtTokenGenerator = IJwtTokenGenerator;

        public async Task<IResult<UserLoginQueryResponse>> Handle(
            UserLoginQueryRequest request,
            CancellationToken cancellationToken
        )
        {
            var user = await identityService.AuthenticateUser(request.Email, request.Password);

            if (user == null)
            {
                return Response<UserLoginQueryResponse>.ErrorResponse(
                    [$"Invalid Credentials for '{request.Email}'."]
                );
            }

            var response = mapper.Map<UserLoginQueryResponse>(user);
            response.JWT = await _IJwtTokenGenerator.GenerateJwtToken(user);

            return Response<UserLoginQueryResponse>.SuccessResponese(response);
        }
    }
}
