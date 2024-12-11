using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Categorias.Queries
{
    public class GetCategoriasQuery : IQuery<List<GetCategoriaReponse>>
    {

    }

    public class GetCategoriaReponse
    {
        public  Guid Id { get; set; }
        public string Nombre { get; set; }
        internal bool OcultaDesdePrincipio { get; set; }
        public List<GetSubcategoriaResponse> Subcategorias { get; set; } = [];
    }

    public class GetSubcategoriaResponse
    {
        public Guid Id { get; set; }
        [JsonIgnore]
        public Guid CategoriaId { get; set; }
        public string Nombre { get; set; }
        public string Imagen => $"/static/media/images/subcategorias/{"string".ToLower()}.jpeg";
    }
}