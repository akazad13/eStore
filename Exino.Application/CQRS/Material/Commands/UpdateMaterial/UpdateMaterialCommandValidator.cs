using FluentValidation;

namespace Exino.Application.CQRS.Material.Commands.UpdateMaterial
{
    public class UpdateMaterialCommandValidator : AbstractValidator<UpdateMaterialCommandRequest>
    {
        public UpdateMaterialCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(50)
                .NotEmpty();
        }
    }
}
