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
        public string Hash { get; set; }
        public bool Spoiler { get; set; }
    }

    public class GetPortadaHomeResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public Guid? Autor { get; set; }
        public bool Spoiler { get; set; }
        public string Miniatura { get; set; }
        [JsonPropertyName("es_nuevo")]
        public bool EsNuevo { get; set; }
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
}
