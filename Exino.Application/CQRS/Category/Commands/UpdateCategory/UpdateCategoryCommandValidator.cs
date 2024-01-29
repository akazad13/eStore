using FluentValidation;

namespace Exino.Application.CQRS.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
        }
    }
}
