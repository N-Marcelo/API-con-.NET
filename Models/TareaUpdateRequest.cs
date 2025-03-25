using System.Text.Json.Serialization;
using webapi.Models;

namespace API_con_.NET.Models;

public class TareaUpdateRequest
{
    public Guid TareaId { get; set; }
    public string? NombreTarea { get; set; }
    public string? DescripcionTarea { get; set; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public Prioridad PrioridadTarea { get; set; }

    public Guid CategoriaId { get; set; }
    public string? NuevoNombreCategoria { get; set; } // Campo opcional
}
