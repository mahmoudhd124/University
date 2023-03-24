using Logic.Dtos.SubjectMaterialDto;
using Logic.MediatR.Commands.SubjectMaterialCommands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Doctor")]
public class SubjectMaterialController : BaseController
{
    [HttpPost]
    public async Task<ActionResult> Add(
        [FromForm] int subjectId, [FromForm] IFormFile file) =>
        Return(await Mediator.Send(new AddSubjectMaterialCommand(
            subjectId, file.OpenReadStream(), file.FileName, Id)));

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectMaterialCommand(id, Id)));
}