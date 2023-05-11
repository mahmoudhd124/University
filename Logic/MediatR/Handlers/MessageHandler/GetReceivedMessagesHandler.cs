using AutoMapper;
using AutoMapper.QueryableExtensions;
using Logic.Data;
using Logic.Dtos.MessageDto;
using Logic.ErrorHandlers;
using Logic.MediatR.Queries.MessgeQueries;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Logic.MediatR.Handlers.MessageHandler;

public class GetReceivedMessagesHandler : IRequestHandler<GetReceivedMessagesQuery, Response<IEnumerable<MessageForReceivedListDto>>>
{
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    public GetReceivedMessagesHandler(IdentityContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Response<IEnumerable<MessageForReceivedListDto>>> Handle(GetReceivedMessagesQuery request,
        CancellationToken cancellationToken)
    {
        var (userId,userName,pageIndex,pageSize) = request;
        var messagesDto = await _context.Messages
            .Where(m => m.ReceiverId.Equals(userId))
            .Skip(pageIndex * pageSize)
            .Take(pageSize)
            .OrderByDescending(m => m.Date)
            .ProjectTo<MessageForReceivedListDto>(_mapper.ConfigurationProvider)
            .ToListAsync(cancellationToken);
        

        var messagesSendersId = messagesDto.Select(m => m.SenderId);

        var messagesSendersIdsToUsernames = await _context.Users
            .Where(u => messagesSendersId.Contains(u.Id))
            .Select(u => new { u.Id, u.UserName })
            .ToListAsync(cancellationToken);

        foreach (var messageDto in messagesDto)
        {
            messageDto.ReceiverUsername = userName;
            messageDto.SenderUsername = messagesSendersIdsToUsernames
                .FirstOrDefault(m => m.Id.Equals(messageDto.SenderId))?.UserName;
        }

        return messagesDto;
    }
}