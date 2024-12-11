using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Features.Hilos.Queries.GetHilo;

namespace Application.Comentarios.GetComentarioDeHilos;

public class GetComentariosDeHiloQuery : IQuery<List<GetComentarioResponse>>
{
    public Guid HiloId { get; set; }
    public DateTime UltimoComentario { get; set; }
}

public class GetComentarioResponse {
    public Guid Id { get; set; }
    public string Texto { get; set; }
    [JsonPropertyName("created_at")]
    public DateTime CreatedAt { get; set; }
    [JsonPropertyName("autor_id")]
    public Guid AutorId { get; set; }

    [JsonPropertyName("es_autor")]
    public bool EsAutor { get; set; }
    public bool Destacado { get; set; }
    public List<string> Taggueos { get; set; } = new List<string>();
    public List<string> Tags { get; set; } = new List<string>();
    [JsonPropertyName("es_op")]
    public bool EsOp { get; set; }
    [JsonPropertyName("recibir_notificaciones")]
    public bool? RecibirNotificaciones { get; set; }
    public string Color {get; set;}
    public GetHiloAutorResponse Autor { get; set; }
    public GetComentarioDetallesResponse Detalles { get; set; }
}


public class GetComentarioDetallesResponse {
    public string Tag { get; set; }
    [JsonPropertyName("tag_unico")]
    public string? TagUnico { get; set; }
    public string? Dados { get; set; }
}
