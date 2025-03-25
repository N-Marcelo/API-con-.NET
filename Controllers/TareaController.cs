using System.Text.Json;
using System.Threading.Tasks;
using API_con_.NET.Models;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TareaController : ControllerBase
{
    ITareasService tareasService;
    
    public TareaController(ITareasService service)
    {
        tareasService = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetTarea()
    {
        var tareas = await tareasService.GetTareaInfoAsync();
        return Ok(tareas);
    }

    [HttpPost]
    public async Task<IActionResult> PostTarea([FromBody] Tarea tarea)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await tareasService.SaveTarea(tarea);
            return Ok("Tarea creada correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al guardar la tarea: {ex.Message}");
        }
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutTarea(Guid id, [FromBody] TareaUpdateRequest request)
    {
        try
        {
            if (id != request.TareaId)
            {
                return BadRequest("El ID de la tarea no coincide.");
            }

            // Crear un objeto Tarea a partir de la solicitud
            var tarea = new Tarea
            {
                TareaId = request.TareaId,
                NombreTarea = request.NombreTarea,
                DescripcionTarea = request.DescripcionTarea,
                PrioridadTarea = request.PrioridadTarea,
                CategoriaId = request.CategoriaId
            };

            // Llamar al servicio para actualizar la tarea
            var resultado = await tareasService.UpdateTarea(id, tarea, request.NuevoNombreCategoria);

            // Si la actualización falla, devolver un error
            if (!resultado)
            {
                return BadRequest("No se pudo actualizar la tarea o la categoría.");
            }

            // Si todo sale bien, devolver una respuesta exitosa
            return Ok("Tarea actualizada correctamente.");
        }
        catch (Exception ex)
        {
            // Manejar cualquier excepción inesperada
            return StatusCode(500, $"Error interno del servidor: {ex.Message}");
        }
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            await tareasService.DeleteTarea(id);
            return Ok("Tarea eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar la tarea: {ex.Message}");
        }
    }
}