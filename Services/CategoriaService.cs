using webapi.Models;

namespace webapi.Services;
public class CategoriaService : ICategoriaService
{
    TareasContext context;

    public CategoriaService(TareasContext dbcontext)
    {
        context = dbcontext;
    }

    //RELOAD
    public IEnumerable<Categoria> GetCategorias()
    {
        return context.Categorias;
    }

    //CREATE
    public async Task SaveCategoria(Categoria categoria)
    {
        context.Add(categoria);
        await context.SaveChangesAsync();
    }
    //UPDATE
    public async Task UpdateCategoria(Guid id, Categoria categoria)
    {
        var categoriaActual = context.Categorias.Find(id);
        if(categoriaActual != null)
        {
            categoriaActual.NombreCategoria = categoria.NombreCategoria;
            categoriaActual.DescripcionCategoria = categoria.DescripcionCategoria;
            categoriaActual.PesoCategoria = categoria.PesoCategoria;

            await context.SaveChangesAsync();
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

}