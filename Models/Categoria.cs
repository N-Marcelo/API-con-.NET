using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace webapi.Models;

public class Categoria
{
    public Guid CategoriaId {get;set;}
    public string? NombreCategoria {get;set;}
    public string? DescripcionCategoria {get;set;}
    public int? PesoCategoria {get;set;}

    [JsonIgnore]
    public virtual ICollection<Tarea>? Tareas {get;set;}
}