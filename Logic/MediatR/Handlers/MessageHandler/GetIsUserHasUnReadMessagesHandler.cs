using Logic.Data;
using Logic.ErrorHandlers;
using Logic.MediatR.Queries.MessgeQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.MessageHandler;

public class GetIsUserHasUnReadMessagesHandler : IRequestHandler<GetIsUserHasUnReadMessagesQuery, Response<bool>>
{
    private readonly IdentityContext _context;

    public GetIsUserHasUnReadMessagesHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(GetIsUserHasUnReadMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var userId = request.UserId;
        return await _context.Messages
            .AnyAsync(m => m.ReceiverId.Equals(userId) && m.Read == false, cancellationToken);
    }
}