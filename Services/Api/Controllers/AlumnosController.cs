using BaseDatos.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers;

[Route("alumnos")]
public class AlumnosController : BaseApiController
{
    private readonly AlumnosRepository repository;
    private const string FOTOS_ALUMNOS = "http://localhost:5000/fotos/alumnos/";
    public AlumnosController(AlumnosRepository repository)
    {
        this.repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> ListarAlumnos()
    {
        var alumnos = await this.repository.ListarTodos();
        return Ok(alumnos);
    }

    [HttpGet("{numControl:long}")]
    public async Task<IActionResult> ObtenerDatosAlumno(long numControl)
    {
        var alumno = await this.repository.ObtenerAlumnoPorNumeroDeControl(numControl);
        if(alumno == null)
        {
            return NotFound($"No existe un alumno con número de control {numControl}");
        }

        return Ok(alumno);
    }

    [HttpPatch("{numControl:long}/foto")]
    public async Task<IActionResult> ActualizarFoto(long numControl)
    {
        //Validar que el alumno exista
        var alumno = await this.repository.ObtenerAlumnoPorNumeroDeControl(numControl);
        if (alumno == null)
        {
            return NotFound($"No existe un alumno con número de control {numControl}");
        }

        //Revisar el content type
        if (!this.Request.HasFormContentType)
        {
            return BadRequest("Debe proporcionar Form content type");
        }
        //Revisar que exista un archivo
        if (this.Request.Form.Files.Count != 0)
        {
            //Validar que el archivo sea una imagen

            //Almacenar el archivo

            //Actualizar la foto del alumno
            await this.repository.ActualizarFoto(numControl, "mi-foto.png");
            return NoContent();
        }

        return BadRequest("Debe proporcionar el archivo de imagen");
    }

    [HttpDelete("{numControl:long}/foto")]
    public async Task<IActionResult> EliminarFoto(long numControl)
    {
        //Validar que el alumno exista
        var alumno = await this.repository.ObtenerAlumnoPorNumeroDeControl(numControl);
        if(alumno == null)
        {
            return NotFound($"No existe un alumno con número de control {numControl}");
        }
        //Eliminar la foto
        await this.repository.EliminarFoto(numControl);

        return NoContent();
    }
}