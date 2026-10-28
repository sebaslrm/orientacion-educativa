using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;

[Route("alumnos")]
public class AlumnosController : Controller
{
    public IActionResult Index()
    {
        return View();
    }

    [Route("{numControl:long}")]
    public IActionResult DatosAlumno(long numControl)
    {
        this.ViewData["NumControl"] = numControl;
        return View();
    }
}