using Logic.Dtos.MessageDto;
using Logic.MediatR.Commands.MessageCommands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class MessageController:BaseController
{
   [HttpPost]
   public async Task<ActionResult> Add([FromBody] AddMessageDto addMessageDto) =>
      Return(await Mediator.Send(new AddMessageCommand(addMessageDto, Id)));
   
   [HttpDelete]
   [Route("{messageId:int}")]
   public async Task<ActionResult> Add(int messageId) =>
      Return(await Mediator.Send(new DeleteMessageCommand(messageId, Id)));
}