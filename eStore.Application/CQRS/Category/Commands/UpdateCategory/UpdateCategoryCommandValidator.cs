using FluentValidation;

namespace eStore.Application.CQRS.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandValidator : AbstractValidator<UpdateCategoryCommandRequest>
    {
        public UpdateCategoryCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(100).NotEmpty();
        }
    }
}
