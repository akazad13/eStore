using eStore.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eStore.Application.CQRS.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public string? Name { get; set; }
    }
}
