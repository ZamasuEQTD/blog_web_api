using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Notificaciones.Queries;

public class GetNotificacionesQuery : IQuery<IEnumerable<GetNotificacionResponse>>
{
    public DateTime? UltimaNotificacion { get; set; }
}

public class GetNotificacionResponse
{
    public string Tipo { get; set; }
    public Guid Id { get; set; }
    public string Contenido { get; set; }
    public DateTime Fecha { get; set; }
    public Guid ComentarioId { get; set; }
    public GetHiloNotificacionResponse Hilo { get; set; }
}

public class GetHiloNotificacionResponse
{
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Imagen { get; set; }
    [JsonIgnore]
    public string Hash { get; set; }
}
