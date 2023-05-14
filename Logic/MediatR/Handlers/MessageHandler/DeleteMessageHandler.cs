using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.MessageCommands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.MessageHandler;

public class DeleteMessageHandler : IRequestHandler<DeleteMessageCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public DeleteMessageHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var (messageId, userId) = request;
        var message = await _context.Messages
            .Where(m => m.Id == messageId)
            .FirstOrDefaultAsync(cancellationToken);

        if (message == null)
            return Response<bool>.Failure(MessageErrors.WrongId);

        if (message.SenderId.Equals(userId) == false)
            return Response<bool>.Failure(MessageErrors.UnAuthorizeDelete);

        _context.Messages.Remove(message);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}