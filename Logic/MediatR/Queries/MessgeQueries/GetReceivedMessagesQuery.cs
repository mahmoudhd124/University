using Logic.Dtos.MessageDto;
using Logic.ErrorHandlers;
using MediatR;

namespace Logic.MediatR.Queries.MessgeQueries;

public record GetReceivedMessagesQuery
    (string UserId, string UserName, int PageIndex, int PageSize) : IRequest<
        Response<IEnumerable<MessageForReceivedListDto>>>;