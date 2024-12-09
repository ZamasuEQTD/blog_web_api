using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;
using Application.Features.Encuestas.Queries.Responses;
using Application.Features.Hilos.Queries.Responses;

namespace Application.Features.Hilos.Queries.GetHilo;

public class GetHiloQuery : IQuery<GetHiloResponse>
{
    public Guid Hilo { get; set; }
}

public class GetHiloResponse
{
    public Guid Id { get; set; }
    [JsonPropertyName("autor_id")]
    public Guid? AutorId { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    [JsonPropertyName("es_op")]
    public bool EsOp { get; set; }
    [JsonPropertyName("cantidad_de_comentarios")]
    public int CantidadComentarios { get; set; }
    [JsonPropertyName("creado_en")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("recibir_notificaciones")]
    public bool? RecibirNotificaciones { get; set; }
    [JsonPropertyName("es_sticky")]
    public bool EsSticky { get; set; }
    public GetEncuestaResponse? Encuesta { get; set; }
    public GetHiloBanderasResponse Banderas { get; set; }
    public GetHiloMediaResponse Media { get; set; }
    public GetHiloAutorResponse Autor { get; set; }
    public GetSubcategoriaResponse Subcategoria { get; set; }
}

 
public class GetHiloMediaResponse
{
    [JsonPropertyName("es_spoiler")]
    public bool Spoileable { get; set; }
    public string Url { get; set; }
    public string? Previsualizacion { get; set; }
}

public class GetHiloAutorResponse
{
    public string Nombre { get; set; }
    public string Rango { get; set; }
    public string RangoCorto { get; set; }
}
