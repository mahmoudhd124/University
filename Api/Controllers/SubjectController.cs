using Logic.Commands.SubjectCommands;
using Logic.Dtos.SubjectDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SubjectController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AddSubjectDto addSubjectDto) =>
        Return(await Mediator.Send(new AddSubjectCommand(addSubjectDto)));
    
    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectCommand(id)));
}