using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;

namespace Application.Moderacion
{
    public class GetRegistroDeComentariosQuery : IQuery<List<GetRegistroDeComentarioResponse>>
    {
        public Guid Usuario { get; set; }
        public DateTime? UltimoComentario { get; set; }
    }

    public class GetRegistroDeComentarioResponse
    {
        public Guid Comentario  {get;set;}
        public GetHiloRegistroResponse Hilo { get; set; }
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
    }

    public class GetHiloRegistroResponse
    {
        public Guid Id { get; set; }
        public string Titulo { get; set; }
        public string Imagen { get; set; }

        [JsonIgnore]
        public string Hash { get; set; }
    }
}