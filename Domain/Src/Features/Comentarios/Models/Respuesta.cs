using SharedKernel.Abstractions;

namespace Domain.Comentarios
{
    public class Respuesta : Entity<RespuestaId>
    {
        public ComentarioId RespondidoId { get; private set; }
        public ComentarioId RespuestaId { get; private set; }
        public Respuesta(ComentarioId respondido, ComentarioId respuesta)
        {
            Id = new RespuestaId(Guid.NewGuid());
            RespondidoId = respondido;
            RespuestaId = respuesta;
        }
        public Respuesta() { }
    }

    public class RespuestaId : EntityId
    {
        public RespuestaId(Guid id) : base(id) { }
        public RespuestaId() : base() { }
    }
}