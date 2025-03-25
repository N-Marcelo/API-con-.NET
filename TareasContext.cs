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
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("f4b5f689-b610-43e5-bfa4-5c75b5dbc171"), NombreCategoria = "Actividades laborales", PesoCategoria = 100});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("690859b2-32be-481c-901a-d9bafaad30ca"), NombreCategoria = "Actividades académicas", PesoCategoria = 90});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("5814cb12-5b08-411a-8b78-d426c6ba2bfd"), NombreCategoria = "Actividades familiares", PesoCategoria = 60});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("eee19361-7099-48a7-9a64-a3aa4b26b2ab"), NombreCategoria = "Actividades sociales", PesoCategoria = 50});
        categoriasInit.Add(new Categoria() { CategoriaId = Guid.Parse("913d165e-2402-4e91-8d84-f3921fac3634"), NombreCategoria = "Actividades personales", PesoCategoria = 40});



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

        // Tareas iniciales sin TareaId (EF lo generará automáticamente)
        tareasInit.Add(new Tarea() 
        { 
            TareaId = Guid.NewGuid(),
            CategoriaId = Guid.Parse("913d165e-2402-4e91-8d84-f3921fac3634"), 
            PrioridadTarea = Prioridad.Media, 
            NombreTarea = "Pago de servicios publicos", 
            FechaCreacionTarea = DateTime.Now 
        });

        tareasInit.Add(new Tarea() 
        { 
            TareaId = Guid.NewGuid(),
            CategoriaId = Guid.Parse("913d165e-2402-4e91-8d84-f3921fac3634"), 
            PrioridadTarea = Prioridad.Baja, 
            NombreTarea = "Terminar de ver pelicula en netflix", 
            FechaCreacionTarea = DateTime.Now 
        });

        modelBuilder.Entity<Tarea>(tarea =>
        {
            tarea.ToTable("Tarea");
            tarea.HasKey(t => t.TareaId);

            // Generar automáticamente el TareaId al insertar una nueva tarea
            tarea.Property(t => t.TareaId).ValueGeneratedOnAdd();

            tarea.HasOne(t => t.Categoria)
                .WithMany(c => c.Tareas)
                .HasForeignKey(t => t.CategoriaId);

            tarea.Property(t => t.NombreTarea).HasMaxLength(200);
            tarea.Property(t => t.DescripcionTarea);
            tarea.Property(t => t.PrioridadTarea).HasConversion<int>();
            tarea.Property(t => t.FechaCreacionTarea);

            // Agregar datos iniciales
            tarea.HasData(tareasInit);
        });

    }

}