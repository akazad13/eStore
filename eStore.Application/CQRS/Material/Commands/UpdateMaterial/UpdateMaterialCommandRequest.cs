using eStore.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eStore.Application.CQRS.Material.Commands.UpdateMaterial
{
    public class UpdateMaterialCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public IFormFile? MaterialImage { get; set; }
    }
}
