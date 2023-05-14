using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SpaController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Index()
    {
        return PhysicalFile($"{Environment.CurrentDirectory}/wwwroot/index.html","text/html");
    }
}