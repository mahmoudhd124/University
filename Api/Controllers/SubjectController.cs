using Logic.Dtos.SubjectDto;
using Logic.MediatR.Commands.SubjectCommands;
using Logic.MediatR.Queries.SubjectQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SubjectController : BaseController
{
    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("{pageIndex:int}/{pageSize:int}")]
    public async Task<ActionResult> Page(int pageIndex, int pageSize, string department, int? year,
        string namePrefix) =>
        Return(await Mediator.Send(new GetSubjectForPageQuery(pageIndex, pageSize, department, year, namePrefix)));

    [Authorize(Roles = "Admin,Doctor")]
    [HttpGet]
    [Route("{code:int}")]
    public async Task<ActionResult> Page(int code) =>
        Return(await Mediator.Send(new GetSubjectByCodeQuery(code, Roles, Id)));

    [Authorize(Roles = "Admin")]
    [HttpPut]
    public async Task<ActionResult> Edit([FromBody] EditSubjectDto editSubjectDto) =>
        Return(await Mediator.Send(new EditSubjectCommand(editSubjectDto)));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AddSubjectDto addSubjectDto) =>
        Return(await Mediator.Send(new AddSubjectCommand(addSubjectDto)));

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult> Delete(int id) =>
        Return(await Mediator.Send(new DeleteSubjectCommand(id)));

    [Authorize(Roles = "Admin")]
    [HttpDelete]
    [Route("DeleteAssignedDoctor/{subjectId:int}")]
    public async Task<ActionResult> DeleteAssignedDoctor(int subjectId) =>
        Return(await Mediator.Send(new DeleteAssignedDoctorCommand(subjectId)));
}