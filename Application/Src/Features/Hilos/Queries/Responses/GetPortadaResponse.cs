using System.Text.Json.Serialization;
using Domain.Features.Medias.Services;

namespace Application.Hilos.Queries.Responses;

public class GetHiloPortadaResponse {
    public Guid Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    [JsonPropertyName("autor_id")]
    public Guid? AutorId { get; set; }
    public string Categoria { get; set; }
    [JsonPropertyName("ultimo_bump")]
    public DateTime UltimoBump { get; set; }
    [JsonPropertyName("es_op")]
    public bool EsOp { get; set; }
    [JsonPropertyName("es_sticky")]
    public bool EsSticky { get; set; }
    public GetHiloPortadaBanderasResponse Banderas { get; set; }
    public GetHiloPortadaImagenResponse Imagen { get; set; }
}

public class GetHiloPortadaBanderasResponse
{
    [JsonPropertyName("dados_activados")]
    public bool Dados { get; set; }
    [JsonPropertyName("id_unico_activado")]
    public bool IdUnico { get; set; }
    [JsonPropertyName("tiene_encuesta")]
    public bool Encuesta { get; set; }
}

public class GetHiloPortadaImagenResponse
{
    [JsonPropertyName("es_spoiler")]
    public bool Spoiler { get; set; }
    public string Miniatura => PortadaHelper.GetMiniatura(this);
    internal string Path { get; set; }
    internal string Tipo { get; set; }
    internal string Hash { get; set; }
}


static public class PortadaHelper {
    public static string GetMiniatura(GetHiloPortadaImagenResponse imagen) {
        return imagen.Tipo == "youtube" ? YoutubeService.GetVideoThumbnailFromUrl(imagen.Path) : "/media/thumbnails/" + imagen.Hash + ".jpeg";
    }
}