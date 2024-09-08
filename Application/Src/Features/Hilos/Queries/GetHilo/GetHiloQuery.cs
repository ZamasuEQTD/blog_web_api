using Application.Abstractions.Messaging;
using Application.Categorias.Queries;

namespace Application.Hilos.Queries
{
    public class GetHiloQuery : IQuery<GetHiloResponse>
    {
        public Guid Hilo { get; set; }
    }

    public class GetHiloResponse
    {
        public bool EsOp { get; set; }
        internal Guid AutorId { get; set; }
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        internal Guid? EncuestaId { get; set; }
        public GetSubcategoriaResponse Subcategoria { get; set; }
        public GetEncuestaResponse? Encuesta { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
    }

    public class GetEncuestaResponse
    {
        public Guid Id { get; set; }
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