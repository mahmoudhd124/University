using Logic.Dtos.MessageDto;
using Logic.MediatR.Commands.MessageCommands;
using Logic.MediatR.Queries;
using Logic.MediatR.Queries.MessgeQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class MessageController : BaseController
{
    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult> Get(int id) =>
        Return(await Mediator.Send(new GetMessageByIdQuery(id, Id)));

    [HttpGet]
    [Route("GetReceived")]
    public async Task<ActionResult> GetReceived(int pageIndex, int pageSize) =>
        Return(await Mediator.Send(new GetReceivedMessagesQuery(Id, Username, pageIndex, pageSize)));

    [HttpGet]
    [Route("GetSend")]
    public async Task<ActionResult> GetSend(int pageIndex, int pageSize) =>
        Return(await Mediator.Send(new GetSendMessagesQuery(Id, Username, pageIndex, pageSize)));

    [HttpGet]
    [Route("CheckUnReadMessages")]
    public async Task<ActionResult> CheckUnReadMessages() =>
        Return(await Mediator.Send(new GetIsUserHasUnReadMessagesQuery(Id)));

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult> Add([FromBody] AddMessageDto addMessageDto) =>
        Return(await Mediator.Send(new AddMessageCommand(addMessageDto, Id)));

    [HttpDelete]
    [Authorize(Roles = "Admin")]
    [Route("{messageId:int}")]
    public async Task<ActionResult> Delete(int messageId) =>
        Return(await Mediator.Send(new DeleteMessageCommand(messageId, Id)));
}