using Exino.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Exino.Application.CQRS.Product.Commands.CreateProduct
{
    public class CreateProductCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public decimal Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public string? SKU { get; set; }
        public IFormFile? UploadPath { get; set; }
        public string? ImagePath { get; set; }
        public int Stock { get; set; } = 0;
        public int CategoryId { get; set; }
    }
}
