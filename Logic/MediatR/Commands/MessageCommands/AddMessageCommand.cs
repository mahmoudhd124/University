using Logic.Dtos.MessageDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Commands.MessageCommands;

public record AddMessageCommand(AddMessageDto AddMessageDto, string SenderId) : IRequest<Response<bool>>;