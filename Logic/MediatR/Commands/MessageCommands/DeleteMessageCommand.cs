using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.MessageCommands;

public record DeleteMessageCommand(int MessageId,string UserId):IRequest<Response<bool>>;