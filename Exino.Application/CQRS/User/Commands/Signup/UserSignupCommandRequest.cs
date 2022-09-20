using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.User.Commands.Signup
{
    public class UserSignupCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? Email { get; set; }
        public string? Password { get; set; }
        public bool IsSubscribeToNewsletter { get; set; }
    }
}
