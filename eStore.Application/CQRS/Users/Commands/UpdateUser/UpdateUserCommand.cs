using eStore.Application.Common.Wrappers;
using MediatR;

namespace eStore.Application.CQRS.Users.Commands.UpdateUser;

public class UpdateUserCommand : IRequest<IResult<GenericResponse>>
{
    public int UserId { get; set; } = 0;
    public string Locale { get; set; } = null!;
}
