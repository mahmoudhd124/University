using Logic.MediatR.Commands.UserCommands;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class UserController : BaseController
{
    [HttpPost]
    [Route("ChangeProfilePhoto")]
    public async Task<ActionResult> ChangeProfilePhoto([FromForm] IFormFile file) =>
        Return(await Mediator.Send(new ChangeUserProfilePhotoCommand(Id, file.FileName, file.OpenReadStream())));
}