using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.AuthCommands;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<Response<bool>>;