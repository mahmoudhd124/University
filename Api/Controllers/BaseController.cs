using System.Security.Claims;
using Logic.ErrorHandlers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[ApiController]
[Route("Api/[controller]")]
public class BaseController : ControllerBase
{
    private IMediator _mediator;

    public IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();

    public string Username => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    public string Id => User?.Claims?.FirstOrDefault(c => c.Type.Equals(ClaimTypes.Sid))?.Value;

    public string Useragent => HttpContext.Request.Headers.UserAgent;
    public ActionResult Return<T>(Response<T> response) =>
     response.IsSuccess ?
        Ok(response.Data) :
        BadRequest(new
        {
            code = response.Error.Code,
            message = response.Error.Message
        });
}