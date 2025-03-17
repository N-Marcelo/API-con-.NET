using Microsoft.EntityFrameworkCore;
using webapi.Models;

namespace webapi;

public class TareasContext: DbContext
{
    
    public DbSet<Categoria> Categorias {get;set;}
    public DbSet<Tarea> Tareas {get;set;}

    public TareasContext(DbContextOptions<TareasContext> options) :base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Categoria> categoriasInit = new List<Categoria>();
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), NombreCategoria = "Actividades pendientes", PesoCategoria = 20});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), NombreCategoria = "Actividades personales", PesoCategoria = 50});


        modelBuilder.Entity<Categoria>(categoria=> 
        {
            categoria.ToTable("Categoria");
            categoria.HasKey(c=> c.CategoriaId);

            categoria.Property(c=> c.NombreCategoria).HasMaxLength(150);

            categoria.Property(c=> c.DescripcionCategoria);

            categoria.Property(c=> c.PesoCategoria);

            categoria.HasData(categoriasInit);
        });

        List<Tarea> tareasInit = new List<Tarea>();

        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb410"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb4ef"), PrioridadTarea = Prioridad.Media, NombreTarea = "Pago de servicios publicos", FechaCreacionTarea = DateTime.Now });
        tareasInit.Add(new Tarea() { TareaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb411"), CategoriaId = Guid.Parse("fe2de405-c38e-4c90-ac52-da0540dfb402"), PrioridadTarea = Prioridad.Baja, NombreTarea = "Terminar de ver pelicula en netflix", FechaCreacionTarea = DateTime.Now });

        modelBuilder.Entity<Tarea>(tarea=>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t=> t.TareaId);

            tarea.HasOne(t=> t.Categoria).WithMany(c=> c.Tareas).HasForeignKey(t=> t.CategoriaId);

            tarea.Property(t=> t.NombreTarea).HasMaxLength(200);

            tarea.Property(t=> t.DescripcionTarea);

            tarea.Property(t=> t.PrioridadTarea);

            tarea.Property(t=> t.FechaCreacionTarea);

            tarea.HasData(tareasInit);

        });

    }

}