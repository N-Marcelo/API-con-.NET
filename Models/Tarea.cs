using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace webapi.Models;

public class Tarea
{
    public Guid TareaId {get;set;}
    public Guid CategoriaId {get;set;}
    public string? NombreTarea {get;set;}
    public string? DescripcionTarea {get;set;}
    public Prioridad? PrioridadTarea {get;set;}
    public DateTime FechaCreacionTarea {get;set;}    
    public virtual Categoria? Categoria {get;set;}
}

public enum Prioridad
{
    Baja = 0,
    Media = 1,
    Alta = 2
}