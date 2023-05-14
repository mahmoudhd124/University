using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

public class SpaController : BaseController
{
    [HttpGet]
    [AllowAnonymous]
    public ActionResult Index()
    {
        var wwwroot = Path.Combine(Environment.CurrentDirectory, "wwwroot");
        return PhysicalFile($"{Path.Combine(wwwroot, "index.html")}", "text/html");
    }
}