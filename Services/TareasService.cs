using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi.Services;
    public class TareasService : ITareasService
    {
        private readonly TareasContext _context;

        public TareasService(TareasContext dbcontext)
        {
            _context = dbcontext;
        }

        //RELOAD
        public async Task<List<TareaInfo>> GetTareaInfoAsync()
        {
            var tareaInfo = await _context.Tareas
                .Include(t => t.Categoria)
                .Select(t => new TareaInfo
                {
                    TareaId = t.TareaId,
                    NombreTarea = t.NombreTarea,
                    DescripcionTarea = t.DescripcionTarea,
                    PrioridadTarea = t.PrioridadTarea,
                    FechaCreacionTarea = t.FechaCreacionTarea,
                    CategoriaId = t.CategoriaId,
                    NombreCategoria = t.Categoria.NombreCategoria
                })
                .ToListAsync();

            return tareaInfo;
        }

        //CREATE
        public async Task SaveTarea(Tarea tarea)
        {
            try
            {
                // Generar un nuevo Guid para TareaId si no se proporciona
                if (tarea.TareaId == Guid.Empty)
                {
                    tarea.TareaId = Guid.NewGuid();
                }

                // Establecer la fecha de creación automáticamente
                tarea.FechaCreacionTarea = DateTime.UtcNow;

                _context.Add(tarea);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                // Registrar el error de clave foránea
                Console.WriteLine($"Error de clave foránea: {ex.InnerException?.Message}");
                throw new Exception("El CategoriaId proporcionado no existe en la tabla Categoria.");
            }
            catch (Exception ex)
            {
                // Registrar otros errores
                Console.WriteLine($"Error al guardar la tarea: {ex.Message}");
                throw;
            }
        }
        //UPDATE
        public async Task<bool> UpdateTarea(Guid id, Tarea tarea, string? nuevoNombreCategoria)
        {
            try
            {
                // Buscar la tarea actual
                var tareaActual = await _context.Tareas.FindAsync(id);
                if (tareaActual == null)
                {
                    Console.WriteLine("Tarea no encontrada.");
                    return false;
                }

                // Actualizar los datos de la tarea
                tareaActual.NombreTarea = tarea.NombreTarea;
                tareaActual.DescripcionTarea = tarea.DescripcionTarea;
                tareaActual.PrioridadTarea = tarea.PrioridadTarea;

                // Si se envía un nuevo nombre de categoría, buscar su ID y actualizar la relación
                if (!string.IsNullOrEmpty(nuevoNombreCategoria))
                {
                    // Buscar la categoría por su nombre
                    var nuevaCategoria = await _context.Categorias
                        .FirstOrDefaultAsync(c => c.NombreCategoria == nuevoNombreCategoria);

                    if (nuevaCategoria == null)
                    {
                        Console.WriteLine("Error: La nueva categoría no existe.");
                        return false;
                    }

                    // Actualizar el CategoriaId de la tarea con el ID de la nueva categoría
                    tareaActual.CategoriaId = nuevaCategoria.CategoriaId;
                }
                else
                {
                    // Si no se envía un nuevo nombre de categoría, validar si la categoría actual existe
                    var categoriaActual = await _context.Categorias.FindAsync(tarea.CategoriaId);
                    if (categoriaActual == null)
                    {
                        Console.WriteLine("Error: La categoría no existe.");
                        return false;
                    }

                    // Solo actualizamos la categoría si es diferente
                    if (tareaActual.CategoriaId != tarea.CategoriaId)
                    {
                        tareaActual.CategoriaId = tarea.CategoriaId;
                    }
                }

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();
                Console.WriteLine("Tarea y/o categoría actualizada correctamente.");
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error al actualizar: {ex.Message}");
                return false;
            }
        }

        //DELETE
        public async Task DeleteTarea(Guid id)
        {
            var tareaActual = _context.Tareas.Find(id);
            if(tareaActual != null)
            {
                _context.Remove(tareaActual);
                await _context.SaveChangesAsync();
            }
        }
    }

    public class TareaInfo
    {
        public Guid TareaId { get; set; }
        public string? NombreTarea { get; set; }
        public string? NombreCategoria { get; set; }
        public string? DescripcionTarea { get; set; }
        public Prioridad? PrioridadTarea { get; set; }
        public DateTime FechaCreacionTarea { get; set; }
        public Guid CategoriaId { get; set; } // Agregar esta propiedad
    }

    public interface ITareasService
    {
        Task<List<TareaInfo>> GetTareaInfoAsync();
        Task SaveTarea(Tarea tarea);
        Task<bool> UpdateTarea(Guid id, Tarea tarea, string? nuevoNombreCategoria);
        Task DeleteTarea(Guid id);
    }