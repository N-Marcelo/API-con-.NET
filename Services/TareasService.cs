using webapi.Models;

namespace webapi.Services;
    public class TareasService : ITareasService
    {
        TareasContext context;

        public TareasService(TareasContext dbcontext)
        {
            context = dbcontext;
        }

        public IEnumerable<Tarea> GetTareas()
        {
            return context.Tareas;
        }

    }

    public interface ITareasService
    {
        IEnumerable<Tarea> GetTareas();
    }