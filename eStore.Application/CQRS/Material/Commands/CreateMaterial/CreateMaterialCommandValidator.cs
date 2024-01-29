using FluentValidation;

namespace eStore.Application.CQRS.Material.Commands.CreateMaterial
{
    public class CreateMaterialCommandValidator : AbstractValidator<CreateMaterialCommandRequest>
    {
        public CreateMaterialCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(50).NotEmpty();
        }
    }
}
