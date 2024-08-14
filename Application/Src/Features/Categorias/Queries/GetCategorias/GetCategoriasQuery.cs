using Application.Abstractions.Messaging;

namespace Application.Categorias.Queries
{
    public class GetCategoriasQuery : IQuery<List<GetCategoriaDto>>
    {

    }

    public class GetCategoriaDto
    {
        public string Nombre { get; set; }
        public List<GetSubcategoriaResponse> Subcategorias { get; set; } = [];
    }

    public class GetSubcategoriaResponse
    {
        public string Nombre { get; set; }
        internal string ImagePath { get; set; }
        public string Imagen => ImagePath;
    }
}