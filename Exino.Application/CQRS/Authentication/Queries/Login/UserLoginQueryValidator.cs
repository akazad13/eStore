using FluentValidation;

namespace Exino.Application.CQRS.Authentication.Queries.Login
{
    public class UserLoginQueryValidator : AbstractValidator<UserLoginQueryRequest>
    {
        public UserLoginQueryValidator()
        {
            RuleFor(v => v.Email)
                .MaximumLength(50)
                .NotEmpty()
                .EmailAddress();
            RuleFor(v => v.Password)
                .NotEmpty();
        }
    }
}
