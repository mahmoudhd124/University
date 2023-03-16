using Logic.Dtos.RoleDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.RoleCommands;

public record AddRoleCommand(AddRoleDto AddRoleDto) : IRequest<Response<bool>>;