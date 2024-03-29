﻿using eStore.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace eStore.Application.CQRS.Material.Commands.CreateMaterial
{
    public class CreateMaterialCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public string? Name { get; set; }
        public IFormFile? MaterialImage { get; set; }
    }
}
