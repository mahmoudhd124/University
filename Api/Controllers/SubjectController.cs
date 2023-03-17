using Logic.Commands.SubjectCommands;
using Logic.Dtos.SubjectDto;
using Logic.Queries.SubjectQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SubjectController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("{pageIndex:int}/{pageSize:int}")]
    public async Task<ActionResult> Page(int pageIndex, int pageSize, string department, int? year) =>
        Return(await Mediator.Send(new GetSubjectForPageQuery(pageIndex, pageSize, department, year)));

    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet]
    [Route("{name}")]
    public async Task<ActionResult> Page(string name) =>
        Return(await Mediator.Send(new GetSubjectByNameQuery(name)));

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