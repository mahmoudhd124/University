using Logic.Dtos.MessageDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.MessgeQueries;

public record GetMessageByIdQuery(int Id, string UserId) : IRequest<Response<MessageDto>>;