using FluentValidation;

namespace Exino.Application.CQRS.AppUser.Commands.UserSignup
{
    public class UserSignupCommandValidator : AbstractValidator<UserSignupCommand>
    {
        public UserSignupCommandValidator()
        {
            RuleFor(v => v.Email)
                .MaximumLength(50)
                .NotEmpty()
                .EmailAddress();
            RuleFor(v => v.Password)
                .NotEmpty();
            RuleFor(v => v.FirstName)
                .MaximumLength(50)
                .NotEmpty();
            RuleFor(v => v.LastName)
               .MaximumLength(50)
               .NotEmpty();
        }
    }
}
