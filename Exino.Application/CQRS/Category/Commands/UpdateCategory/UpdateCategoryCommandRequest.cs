using Exino.Application.Common.Wrappers;
using MediatR;

namespace Exino.Application.CQRS.Category.Commands.UpdateCategory
{
    public class UpdateCategoryCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
    }
}
