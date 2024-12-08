using System.Text.Json.Serialization;
using Application.Features.Hilos.Queries.Responses;

namespace Application.Hilos.Queries.Responses;

public class GetHiloPortadaResponse {
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    [JsonPropertyName("autor_id")]
    public Guid? AutorId { get; set; }
    [JsonPropertyName("ultimo_bump")]
    public DateTime UltimoBump { get; set; }
    [JsonPropertyName("es_op")]
    public bool EsOp { get; set; }
    [JsonPropertyName("es_sticky")]
    public bool EsSticky { get; set; }
    [JsonPropertyName("es_nuevo")]
    public bool EsNuevo => CreatedAt.AddMinutes(20) > DateTime.UtcNow;
    internal   DateTime CreatedAt { get; set; }
    public string Subcategoria { get; set; }
    [JsonPropertyName("recibir_notificaciones")]
    public bool? RecibirNotificaciones { get; set; }
    public GetHiloBanderasResponse Banderas { get; set; }
    public GetHiloPortadaImagenResponse Miniatura { get; set; }
}

 
public class GetHiloPortadaImagenResponse
{
    [JsonPropertyName("es_spoiler")]
    public bool Spoiler { get; set; }
    public string Url { get; set; }
}
 
