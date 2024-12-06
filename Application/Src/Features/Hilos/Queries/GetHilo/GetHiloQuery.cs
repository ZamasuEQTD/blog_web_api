using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;

namespace Application.Hilos.Queries
{
    public class GetHiloQuery : IQuery<GetHiloResponse>
    {
        public Guid Hilo { get; set; }
    }

    public class HiloResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Guid SubcategoriaId { get; set; }
        public string Nombre { get; set; }
        public DateTime CreatedAt { get; set; }
        public Guid AutorId { get; set; }
        public string Hash { get; set; }
        public string Path { get; set; }
        public string TipoDeArchivo { get; set; }
        public bool Spoiler { get; set; }
        public bool Dados { get; set; }
        public bool IdUnico { get; set; }
        public int Comentarios { get; set; }
    }

    public class RespuestaResponse
    {
        public Guid Id { get; set; }
        public Guid EncuestaId { get; set; }
        public int Votos { get; set; }
        public string Contenido { get; set; }
    }
    public class GetHiloResponse
    {
        public Guid Id { get; set; }
        [JsonPropertyName("autor_id")]
        public Guid? AutorId { get; set; }
        [JsonPropertyName("es_op")]
        public bool EsOp { get; set; }
        public string Titulo { get; set; }
        [JsonPropertyName("cantidad_de_comentarios")]
        public int Comentarios { get; set; }
        public string Descripcion { get; set; }
        [JsonPropertyName("creado_en")]
        public DateTime CreatedAt { get; set; }
        public GetAutorResponse Autor { get; set; }
        public GetSubcategoriaResponse Subcategoria { get; set; }

        [JsonPropertyName("portada")]
        public GetMediaSpoileable<GetMediaResponse> Media { get; set; }
        public BanderasResponse Banderas { get; set; }
        public GetEncuestaResponse? Encuesta { get; set; }
    }
    

    public class BanderasResponse {
        public bool Dados { get; set; }
        [JsonPropertyName("id_unico")]
        public bool IdUnico { get; set; }
    }

    public class GetMediaSpoileable<T> 
    {
        public bool Spoiler { get; set; }
        public T Media { get; set; }

    }

    public class GetMediaResponse
    {
        public string? Previsualizacion { get; set; }
        public string Tipo { get; set; }
        public string Url { get; set; }
    }

    public class GetAutorResponse
    {
        public string Nombre { get; set; }
        public string Rango { get; set; }
    }

    public class GetEncuestaResponse
    {
        public Guid Id { get; set; }
        [JsonPropertyName("opcion_votada")]
        public Guid? OpcionVotada { get; set; }
        public List<GetOpcionResponse> Opciones { get; set; }
    }

    public class GetOpcionResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public int Votos { get; set; }
    }
}