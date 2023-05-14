using Logic.Dtos.DoctorDto;
using Logic.MediatR.Commands.DoctorCommands;
using Logic.MediatR.Commands.DoctorSubjectCommands;
using Logic.MediatR.Queries.DoctorQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class DoctorController : BaseController
{
    [HttpGet]
    [Route("{id?}")]
    public async Task<ActionResult> Get(string id) =>
        Return(await Mediator.Send(new GetDoctorQuery(id ?? Id, Id)));

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("{pageIndex:int}/{pageSize:int}/{usernamePrefix?}")]
    public async Task<ActionResult> Page(int pageIndex, int pageSize, bool? hasSubject = null,
        string usernamePrefix = "") =>
        Return(await Mediator.Send(new GetDoctorsPageQuery(pageSize, pageIndex, usernamePrefix, hasSubject)));

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("Report/{id}")]
    public async Task<ActionResult> GetReport(string id) =>
        Return(await Mediator.Send(new GetDoctorReportQuery(id)));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AddDoctorDto addDoctorDto) =>
        Return(await Mediator.Send(new AddDoctorCommand(addDoctorDto)));

    [HttpPut]
    [Authorize(Roles = "Doctor")]
    public async Task<ActionResult> Edit([FromBody] EditDoctorDto editDoctorDto) =>
        Return(await Mediator.Send(new EditDoctorCommand(editDoctorDto, Id)));

    [Authorize(Roles = "Admin")]
    [Route("{id}")]
    [HttpDelete]
    public async Task<ActionResult> Delete(string id) =>
        Return(await Mediator.Send(new DeleteDoctorCommand(id)));

    [Route("GetEditInfo/{id}")]
    [HttpGet]
    public async Task<ActionResult> GetEditInfo(string id) =>
        Return(await Mediator.Send(new GetEditDoctorDataQuery(id)));


    [Authorize(Roles = "Admin")]
    [HttpGet]
    [Route("AssignToSubject/{doctorId}/{subjectId:int}")]
    public async Task<ActionResult> AssignToTask(string doctorId, int subjectId) =>
        Return(await Mediator.Send(new AssignDoctorToSubjectCommand(doctorId, subjectId)));
}