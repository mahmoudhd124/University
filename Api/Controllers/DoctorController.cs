using Logic.Commands.DoctorCommands;
using Logic.Dtos.DoctorDto;
using Logic.Queries.DoctorQueries;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class DoctorController : BaseController
{
    [HttpGet]
    [Route("{id:guid}")]
    public async Task<ActionResult> Get(string id) =>
        Return(await Mediator.Send(new GetDoctorQuery(id)));

    [HttpGet]
    [Authorize(Roles = "Admin")]
    [Route("{pageIndex:int}/{pageSize:int}/{usernamePrefix?}")]
    public async Task<ActionResult> Page(int pageIndex, int pageSize, string usernamePrefix = "") =>
        Return(await Mediator.Send(new GetDoctorsPageQuery(pageSize, pageIndex, usernamePrefix)));

    [Authorize(Roles = "Admin")]
    [HttpPost]
    public async Task<ActionResult> Add([FromBody] AddDoctorDto addDoctorDto) =>
        Return(await Mediator.Send(new AddDoctorCommand(addDoctorDto)));

    [HttpPut]
    public async Task<ActionResult> Edit([FromBody] EditDoctorDto editDoctorDto) =>
        Return(await Mediator.Send(new EditDoctorCommand(editDoctorDto, Id)));

    [Authorize(Roles = "Admin")]
    [Route("{id:guid}")]
    [HttpDelete]
    public async Task<ActionResult> Delete(string id) => 
        Return(await Mediator.Send(new DeleteDoctorCommand(id)));
}