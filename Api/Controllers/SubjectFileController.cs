using Logic.Dtos.SubjectMaterialDto;
using Logic.MediatR.Commands.SubjectMaterialCommands;
using Logic.MediatR.Queries.SubjectMaterialsQueries;
using Logic.Models.IdentityModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Authorize(Roles = "Admin,Doctor")]
public class SubjectFileController : BaseController
{
    [HttpGet]
    [Route("{name}/{returnName?}")]
    public async Task<ActionResult> Get(string name, string? returnName)
    {
        var response = await Mediator.Send(new GetSubjectMaterialPathAndContentQuery(name));
        if (response.IsSuccess == false)
            return Return(response);

        return File(response.Data.Bytes, "application/octet-stream", returnName ?? name);
    }

    [HttpPost]
    [Authorize(Roles = "Doctor")]
    public async Task<ActionResult> Add([FromForm] IFormFile file,
        [FromForm] AddSubjectMaterialDto addSubjectMaterialDto) =>
        Return(await Mediator.Send(new AddSubjectMaterialCommand(
            addSubjectMaterialDto, file.OpenReadStream(), file.FileName, Id)));

    [HttpDelete]
    [Authorize(Roles = "Doctor")]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectMaterialCommand(id, Id)));
}