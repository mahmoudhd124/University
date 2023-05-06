using Logic.Dtos.SubjectMaterialDto;
using Logic.MediatR.Commands.SubjectMaterialCommands;
using Logic.MediatR.Queries.SubjectMaterialsQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Doctor")]
public class SubjectMaterialController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    [Route("{name}/{returnName?}")]
    public async Task<ActionResult> Get(string name, string? returnName)
    {
        var response = await Mediator.Send(new GetSubjectMaterialPathAndContentQuery(name));
        if (response.IsSuccess == false)
            return Return(response);

        return File(response.Data.Bytes, "application/octet-stream",returnName ?? name);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromForm] int subjectId, [FromForm] IFormFile file) =>
        Return(await Mediator.Send(new AddSubjectMaterialCommand(
            subjectId, file.OpenReadStream(), file.FileName, Id)));

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectMaterialCommand(id, Id)));
}