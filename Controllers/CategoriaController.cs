using Microsoft.AspNetCore.Mvc;
using webapi.Models;
using webapi.Services;

namespace webapi.Controllers;

[Route("api/[controller]")]
public class CategoriaController: ControllerBase
{
    ICategoriaService categoriaService;

    public CategoriaController(ICategoriaService service)
    {
        categoriaService = service;
    }

    [HttpGet("categorias-simplificadas")]
    public IActionResult GetCategoriasSimplificadas()
    {
        var categorias = categoriaService.GetCategoriasSimplificadas();
        return Ok(categorias);
    }

    [HttpGet]
    public IActionResult GetCategoria()
    {
        return Ok(categoriaService.GetCategorias());
    }

    [HttpPost]
    public async Task<IActionResult> PostCategoria([FromBody] Categoria categoria)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            await categoriaService.SaveCategoria(categoria);
            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al guardar la categorial: {ex.Message}");
        }
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> PutCategoria(Guid id, [FromBody] Categoria categoria)
    {
        try
        {
            if (id != categoria.CategoriaId)
            {
                return BadRequest("El ID de la categoria no coincide.");
            }

            await categoriaService.UpdateCategoria(id, categoria);

            return Ok("Categoria actualizada correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Erro interno del servidor: {ex.Message}");
        }
 
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteCategoria(Guid id)
    {
        try
        {
            await categoriaService.DeleteCategoria(id);
            return Ok("Categoria eliminada correctamente.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Error al eliminar la categoria: {ex.Message}");
        }
    }
}