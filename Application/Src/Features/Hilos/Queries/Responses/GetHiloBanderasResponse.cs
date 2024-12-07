using System.Text.Json.Serialization;

namespace Application.Features.Hilos.Queries.Responses;

public class GetHiloBanderasResponse
{
    [JsonPropertyName("dados_activados")]
    public bool DadosActivados { get; set; }
    [JsonPropertyName("id_unico_activado")]
    public bool IdUnicoActivado { get; set; }
    [JsonPropertyName("tiene_encuesta")]
    public bool TieneEncuesta { get; set; }
}