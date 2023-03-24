using Logic.Dtos.RoleDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.RoleCommands;

public record AddRoleCommand(AddRoleDto AddRoleDto) : IRequest<Response<bool>>;