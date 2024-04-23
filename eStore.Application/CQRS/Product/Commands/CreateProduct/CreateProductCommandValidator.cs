using FluentValidation;

namespace eStore.Application.CQRS.Product.Commands.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(v => v.Name).MaximumLength(200).NotEmpty();
    }
}
