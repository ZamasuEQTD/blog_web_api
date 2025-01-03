using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Moderacion
{
    public class GetRegistroDeComentariosQuery : IQuery<List<GetRegistroDeComentarioResponse>>
    {
        public Guid Usuario { get; set; }
        public Guid? UltimoComentario { get; set; }
    }

    public class GetRegistroDeComentarioResponse
    {
        public Guid Id { get; set; }
        public GetHiloRegistroResponse Hilo { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
    }

    public class GetHiloRegistroResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public GetHiloMiniaturaResponse Miniatura { get; set; }
    }

    public class GetHiloMiniaturaResponse
    {
        public string Provider { get; set; }
        public string Miniatura { get; set; }
    }
}