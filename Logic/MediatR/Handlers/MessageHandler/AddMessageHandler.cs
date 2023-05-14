using AutoMapper;
using Logic.Data;
using Logic.ErrorHandlers;
using Logic.ErrorHandlers.Errors;
using Logic.MediatR.Commands.MessageCommands;
using Logic.Models;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.MessageHandler;

public class AddMessageHandler : IRequestHandler<AddMessageCommand, Response<bool>>
{
    private readonly IdentityContext _context;

    public AddMessageHandler(IdentityContext context)
    {
        _context = context;
    }

    public async Task<Response<bool>> Handle(AddMessageCommand request, CancellationToken cancellationToken)
    {
        var (addMessageDto, senderId) = request;

        var receiverFound =
            await _context.Users.AnyAsync(u => u.Id.Equals(addMessageDto.ReceiverId), cancellationToken);
        if (receiverFound == false)
            return Response<bool>.Failure(UserErrors.WrongId);

        var message = new Message()
        {
            SenderId = senderId,
            ReceiverId = addMessageDto.ReceiverId,
            Title = addMessageDto.Title,
            Content = addMessageDto.Content,
        };

        _context.Messages.Add(message);
        await _context.SaveChangesAsync(cancellationToken);

        return true;
    }
}