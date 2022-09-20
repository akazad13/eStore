using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.User.Queries.Login
{
    public class UserLoginQueryRequest : IRequest<IResult<UserLoginQueryResponse>>
    {
        public string? Email { get; set; }
        public string? Password { get; set; }
    }
}
