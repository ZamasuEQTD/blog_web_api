using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;
using static Domain.Usuarios.Usuario;

namespace Application.Hilos.Queries
{
    public class GetPortadasHomeQuery : IQuery<List<GetPortadaHomeResponse>>
    {
        public string? Titulo { get; set; }
        public DateTime? UltimoBump { get; set; }
        public Guid? Categoria { get; set; }
    }

    public class PortadaResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid Autor { get; set; }
        public Guid SubcategoriaId { get; set; }
        public string NombreDeCategoria { get; set; }
        public Guid? Encuesta { get; set; }
        public bool Dados { get; set; }
        public bool IdUnico { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UltimoBump { get; set; }
        public string TipoDeArchivo { get; set; }
        public string Path { get; set; }
        public string Hash { get; set; }
        public bool Spoiler { get; set; }
        public bool Sticky { get; set; }
        public string AutorNombre { get; set; }
        public string RangoCorto { get; set; }
        public string Rango { get; set; }
    }

    public class GetPortadaHomeResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        [JsonPropertyName("autor_id")]
        public Guid? AutorId { get; set; }
        public GetAutor Autor { get; set; }
        public bool Spoiler { get; set; }
        public string Miniatura { get; set; }
        [JsonPropertyName("ultimo_bump")]
        public DateTime UltimoBump { get; set; }
        [JsonPropertyName("es_op")]
        public bool EsOp { get; set; }

        [JsonPropertyName("es_nuevo")]
        public bool EsNuevo { get; set; }
        [JsonPropertyName("es_sticky")]
        public bool EsSticky {get;set;}
        public GetSubcategoria Subcategoria { get; set; }
        public GetBanderas Banderas { get; set; }
    }

    public class GetSubcategoria
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
    }

    public class GetBanderas
    {
        public bool Dados { get; set; }
        [JsonPropertyName("id_unico")]
        public bool IdUnico { get; set; }
        public bool Encuesta { get; set; }
    }

    public class GetAutor
    {
        public string Nombre { get; set; }
        [JsonPropertyName("rango_corto")]
        public string RangoCorto { get; set; }
        public string Rango { get; set; }
    }
}
