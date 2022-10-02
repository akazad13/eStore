using FluentValidation;

namespace Exino.Application.CQRS.Authentication.Commands.Signup
{
    public class UserSignupCommandValidator : AbstractValidator<UserSignupCommandRequest>
    {
        public UserSignupCommandValidator()
        {
            RuleFor(v => v.Email).MaximumLength(50).NotEmpty().EmailAddress();
            RuleFor(v => v.Password).NotEmpty();
            RuleFor(v => v.FirstName).MaximumLength(50).NotEmpty();
            RuleFor(v => v.LastName).MaximumLength(50).NotEmpty();
        }
    }
}
