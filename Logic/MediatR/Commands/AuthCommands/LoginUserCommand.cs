using Logic.Dtos.AuthDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.AuthCommands;

public record LoginUserCommand(LoginUserDto LoginUserDto, string UserAgent) : IRequest<Response<RefreshTokenDto>>;