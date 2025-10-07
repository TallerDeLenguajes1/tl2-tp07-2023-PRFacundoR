using Microsoft.AspNetCore.Mvc;

namespace Tare.Controllers;// para que sirve

[ApiController]
[Route("[controller]")]
public class TareaController : ControllerBase
{

    private ManejoTareas _manejo;
    public TareaController()
    {
        _manejo = new ManejoTareas();
    }



    [HttpPost("crear")]
    public ActionResult<Tarea> CrearNuevaTarea([FromQuery] int id, [FromQuery] string titulo,[FromQuery] string descripcion, [FromQuery] Estado estado)
    {
        if (_manejo.Tareas.Any(t => t.Id == id))
            return BadRequest($"Ya existe una tarea con Id {id}");

        _manejo.CrearTarea(id, titulo, descripcion, estado);
        return Ok("Se creo con exito la tarea");
    }

    // Obtener una tarea por Id
    [HttpGet("obtener")]
    public ActionResult<Tarea> ObtenerPorId([FromQuery] int id)
    {
        var tarea = _manejo.ObtenerTarea(id);
        if (tarea == null)
            return NotFound($"No se encontró tarea con Id {id}");

        return Ok(tarea);
    }

    // Actualizar estado de una tarea
    [HttpPut("actualizar")]
    public ActionResult<Tarea> ActualizarEstado([FromQuery] int id, [FromQuery] Estado nuevoEstado)
    {
        var tarea = _manejo.ObtenerTarea(id);
        if (tarea == null)
            return NotFound($"No se encontró tarea con Id {id}");

        _manejo.ActualizarTarea(id, nuevoEstado);

        // Verificación
        var tareaActualizada = _manejo.ObtenerTarea(id);
        if (tareaActualizada.Estado != nuevoEstado)
            return BadRequest("No se pudo actualizar el estado de la tarea");

        return Ok(tareaActualizada);
    }

    // Eliminar una tarea
    [HttpDelete("eliminar")]
    public ActionResult EliminarPorId([FromQuery] int id)
    {
        var tarea = _manejo.ObtenerTarea(id);
        if (tarea == null)
            return NotFound($"No se encontró tarea con Id {id}");

        _manejo.eliminarTarea(id);

        // Verificación
        if (_manejo.ObtenerTarea(id) != null)
            return BadRequest("No se pudo eliminar la tarea");

        return Ok($"Tarea con Id {id} eliminada correctamente");
    }

    // Listar todas las tareas
    [HttpGet("listar")]
    public ActionResult<List<Tarea>> ListarTodas()
    {
        return Ok(_manejo.ListarTareas());
    }

    // Listar todas las tareas completadas
    [HttpGet("listarCompletadas")]
    public ActionResult<List<Tarea>> ListarCompletadas()
    {
        return Ok(_manejo.ListarTareasCompletadas());
    }




}