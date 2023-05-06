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
    [Route("{name}")]
    public async Task<ActionResult> Get(string name)
    {
        var response = await Mediator.Send(new GetSubjectMaterialPathAndContentQuery(name));
        if (response.IsSuccess == false)
            return Return(response);
        
        var fileBytes = response.Data.Bytes;
        var mimeType = "application/octet-stream"; // Specify the appropriate MIME type for your file

        var memoryStream = new MemoryStream(fileBytes);
        // return File(memoryStream, mimeType, name);

        return Ok(File(response.Data.Bytes, "application/octet-stream"));
        // return Ok(response.Data.Bytes);
    }

    [HttpPost]
    public async Task<ActionResult> Add([FromForm]int subjectId, [FromForm] IFormFile file) =>
        Return(await Mediator.Send(new AddSubjectMaterialCommand(
            subjectId, file.OpenReadStream(), file.FileName, Id)));

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectMaterialCommand(id, Id)));
}