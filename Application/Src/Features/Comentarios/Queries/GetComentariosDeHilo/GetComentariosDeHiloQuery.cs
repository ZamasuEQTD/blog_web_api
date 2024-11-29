using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Comentarios
{
    public class GetComentariosDeHiloQuery : IQuery<List<GetComentarioResponse>>
    {
        public Guid Hilo { get; set; }
        public DateTime? UltimoComentario { get; set; }
    }

    public class GetComentarioResponse
    {
        public Guid Id { get; set; }
        [JsonPropertyName("created_at")]
        public DateTime CreatedAt { get; set; }
        public bool Destacado { get; set; }
        [JsonPropertyName("es_autor")]
        public bool EsAutor { get; set; }
        [JsonPropertyName("autor_id")]
        public Guid? AutorId { get; set; }
        [JsonPropertyName("recibir_notificaciones")]
        public bool? RecibirNotificaciones { get; set; }
        public string Texto { get; set; }
        public string Tag { get; set; }
        public string Color {get; set;}
        [JsonPropertyName("tag_unico")]
        public string? TagUnico { get; set; }
        public string? Dados { get; set; }
        public GetComentarioAutorResponse? Autor { get; set; }
    }
     
    public class GetComentarioAutorResponse
    {
        public string Nombre { get; set; }
        public string Rango { get; set; }
        [JsonPropertyName("rango_corto")]
        public string RangoCorto { get; set; }
    }
}