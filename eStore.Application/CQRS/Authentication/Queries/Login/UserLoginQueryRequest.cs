using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryRequest : IRequest<IResult<UserLoginQueryResponse>>
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
