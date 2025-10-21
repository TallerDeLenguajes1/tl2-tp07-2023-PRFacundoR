using Microsoft.AspNetCore.Mvc;


namespace Tare.Controllers;

[ApiController]
[Route("[controller]")]


public class TvController : ControllerBase
{
    private ManejoDeDatos manejo;

    public TvController()
    {
        manejo = new ManejoDeDatos();
    }

    [HttpGet("ListarProgramas")]

    public ActionResult<List<TvProgram>> ListarTodas()
    {
        return Ok(manejo.listar());
    }



    [HttpPost("crear")]
    public IActionResult CrearNuevoPrograma([FromQuery] int id, [FromQuery] string titulo, [FromQuery] string genero, [FromQuery] float duracion, [FromQuery] float inicio, [FromQuery] Dias dia)
    {
        var t = manejo.existe(id, titulo, genero,duracion , inicio, dia);
        if (t == null)
        {
            return BadRequest($"No se puede crear esa tarea");
        }
        else
        {
            return Ok("Se creo el programa");
        }

    }

    [HttpDelete("/api/programs/{id}")]

    public IActionResult BorrarPrograma([FromRoute]int id)
    {
        if (!manejo.existe(id))
        {
            return NotFound("No existe este programa");
        }
        else
        {
            manejo.eliminarPrograma(id);
            return Ok("Se borro con exito");
        }

    }

    [HttpGet("/api/programs/by-day/{day}")]

    public ActionResult<List<TvProgram>> ListarCompletadas([FromRoute] Dias dia)
    {

        return Ok(manejo.listarProgramas(dia));
    }


    [HttpPut("/api/programs/{id}")]

    public IActionResult actualizaPorId([FromRoute] int id, int duracion)
    {
        if (manejo.existe(id))
        {
            manejo.modificarPrograma(id, duracion);
            return Ok("Se modifico perfectamente");

        }else
        {
            return NotFound("No existe este programa");
        }
        
    }

}
