using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Authentication.Commands.Signup
{
    public class UserSignupCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public required string FirstName { get; set; }
        public required string LastName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public bool IsSubscribeToNewsletter { get; set; }
    }
}
