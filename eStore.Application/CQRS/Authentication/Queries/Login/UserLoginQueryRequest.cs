using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryRequest : IRequest<IResult<UserLoginQueryResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
