using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services;
    public class TareasService : ITareasService
    {
        TareasContext context;

        public TareasService(TareasContext dbcontext)
        {
            context = dbcontext;
        }

        //RELOAD
        public async Task<List<TareaInfo>> GetTareaInfoAsync()
        {
            var tareaInfo = await context.Tareas
            .Include(t => t.Categoria)
            .Select(t => new TareaInfo
            {
                TareaId = t.TareaId,
                NombreTarea = t.NombreTarea,
                NombreCategoria = t.Categoria.NombreCategoria,
                DescripcionTarea = t.DescripcionTarea,
                PrioridadTarea = t.PrioridadTarea,
                FechaCreacionTarea = t.FechaCreacionTarea
            })
            .ToListAsync();

        return tareaInfo;
        }

        //CREATE
        public async Task SaveTarea(Tarea tarea)
        {
            context.Add(tarea);
            await context.SaveChangesAsync();
        }
        //UPDATE
        public async Task UpdateTarea(Guid id, Tarea tarea)
        {
            var tareaActual = context.Tareas.Find(id);
            if(tareaActual != null)
            {
                tareaActual.NombreTarea = tarea.NombreTarea;
                tareaActual.DescripcionTarea = tarea.DescripcionTarea;
                tareaActual.PrioridadTarea = tarea.PrioridadTarea;
                tareaActual.Categoria = tarea.Categoria;

                await context.SaveChangesAsync();
            }
        }
        //DELETE
        public async Task DeleteTarea(Guid id)
        {
            var tareaActual = context.Tareas.Find(id);
            if(tareaActual != null)
            {
                context.Remove(tareaActual);
                await context.SaveChangesAsync();
            }
        }
    }

    public class TareaInfo
    {
        public Guid TareaId {get;set;}
        public string? NombreTarea {get;set;}
        public string? NombreCategoria {get;set;}
        public string? DescripcionTarea {get;set;}
        public Prioridad? PrioridadTarea {get;set;}
        public DateTime FechaCreacionTarea {get;set;}    
    }

    public interface ITareasService
    {
        Task<List<TareaInfo>> GetTareaInfoAsync();
        Task SaveTarea(Tarea tarea);
        Task UpdateTarea(Guid id, Tarea tarea);
        Task DeleteTarea(Guid id);
    }