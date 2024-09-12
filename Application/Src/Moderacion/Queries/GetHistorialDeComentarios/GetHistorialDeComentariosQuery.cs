using Application.Abstractions.Messaging;

namespace Application.Moderacion
{
    public class GetHistorialDeComentariosQuery : IQuery<List<GetHistorialDeComentarioResponse>>
    {
        public Guid Usuario { get; set; }
        public int Pagina { get; set; }
    }

    public class GetHistorialDeComentarioResponse
    {
        public Guid Id { get; set; }
        public GetHiloResponse Hilo { get; set; }
        public string Titulo { get; set; }
        public string Texto { get; set; }
    }

    public class GetHiloResponse
    {
        public Guid Id { get; set; }
        public string Imagen { get; set; }
    }
}