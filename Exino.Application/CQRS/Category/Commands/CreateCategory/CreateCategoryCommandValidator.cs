using FluentValidation;

namespace Exino.Application.CQRS.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandValidator : AbstractValidator<CreateCategoryCommandRequest>
    {
        public CreateCategoryCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
        }
    }
}
