using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.Commands.AuthCommands;

public record RegisterUserCommand(RegisterUserDto RegisterUserDto) : IRequest<Response<bool>>;