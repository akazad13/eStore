﻿using Exino.Application.Common.Wrappers;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace Exino.Application.CQRS.Category.Commands.CreateCategory
{
    public class CreateCategoryCommandRequest : IRequest<IResult<GenericResponse>>
    {
        public string? Name { get; set; }
    }
}
