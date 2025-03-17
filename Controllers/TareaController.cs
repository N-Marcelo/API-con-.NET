using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
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
    public IActionResult PostTarea([FromBody] Tarea tarea)
    {
        tareasService.SaveTarea(tarea);
        return Ok();
    }

    [HttpPut("{id}")]
    public IActionResult PutTarea(Guid id, [FromBody] Tarea tarea)
    {
        tareasService.UpdateTarea(id, tarea);
        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        tareasService.DeleteTarea(id);
        return Ok();
    }
}