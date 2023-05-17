using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.AuthCommands;

public record ChangePasswordCommand(string UserId,ChangePasswordDto ChangePasswordDto):IRequest<Response<bool>>;