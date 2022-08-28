﻿using FluentValidation;

namespace Exino.Application.CQRS.AppUser.Commands.UserLogin
{
    public class UserLoginCommandValidator : AbstractValidator<UserLoginCommand>
    {
        public UserLoginCommandValidator()
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
