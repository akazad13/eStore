using FluentValidation;

namespace Exino.Application.CQRS.Product.Commands.CreateProduct
{
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
        }
    }
}
