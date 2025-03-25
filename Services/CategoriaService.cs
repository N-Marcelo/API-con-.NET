using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services;
public class CategoriaService : ICategoriaService
{
    TareasContext context;

    public CategoriaService(TareasContext dbcontext)
    {
        context = dbcontext;
    }

    //RELOAD CATEGORIA
    public IEnumerable<object> GetCategoriasSimplificadas()
{
    return context.Categorias
        .Select(c => new
        {
            Id = c.CategoriaId,
            Nombre = c.NombreCategoria
        })
        .ToList();
    }   
    //RELOAD
    public IEnumerable<Categoria> GetCategorias()
    {
        return context.Categorias;
    }

    //CREATE
    public async Task SaveCategoria(Categoria categoria)
    {
        try
        {
            if(categoria.CategoriaId == Guid.Empty)
            {
                categoria.CategoriaId = Guid.NewGuid();
            }

            context.Add(categoria);
            await context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            System.Console.WriteLine($"Error al guardar la categoria: {ex.Message}");
            throw;
        }
    }
    //UPDATE
    public async Task UpdateCategoria(Guid id, Categoria categoria)
    {   
        // Buscar la categoria actual
        var categoriaActual = await context.Categorias.FindAsync(id);
        if(categoriaActual != null)
        {
            categoriaActual.NombreCategoria = categoria.NombreCategoria;
            categoriaActual.DescripcionCategoria = categoria.DescripcionCategoria;
            categoriaActual.PesoCategoria = categoria.PesoCategoria;

            await context.SaveChangesAsync();
        }
        
        else
        {
            System.Console.WriteLine("Categoria no encontrada.");
        }
    }

    //DELETE
    public async Task DeleteCategoria(Guid id)
    {
        var categoriaActual = context.Categorias.Find(id);

        if(categoriaActual != null)
        {
            context.Remove(categoriaActual);
            await context.SaveChangesAsync();
        }
    }
}

public interface ICategoriaService
{
    IEnumerable<Categoria> GetCategorias();
    Task SaveCategoria(Categoria categoria);
    Task UpdateCategoria(Guid id, Categoria categoria);
    Task DeleteCategoria(Guid id);
    IEnumerable<object> GetCategoriasSimplificadas();

}