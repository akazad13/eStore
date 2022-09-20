using FluentValidation;

namespace Exino.Application.CQRS.Product.Commands.CreateProduct
{ 
    public class CreateProductCommandValidator : AbstractValidator<CreateProductCommandRequest>
    {
        public CreateProductCommandValidator()
        {
            RuleFor(v => v.Name)
                .MaximumLength(200)
                .NotEmpty();
            RuleFor(v => v.Description)
                .NotEmpty();
            RuleFor(v => v.Price)
                .NotEmpty();
            RuleFor(v => v.Size)
               .NotEmpty();
            RuleFor(v => v.Color)
               .NotEmpty();
            RuleFor(v => v.SKU)
               .NotEmpty();
            RuleFor(v => v.CategoryId)
               .NotEmpty();
        }
    }
}
