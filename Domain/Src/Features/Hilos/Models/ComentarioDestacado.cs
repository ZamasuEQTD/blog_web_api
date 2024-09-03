using Domain.Comentarios;
using SharedKernel.Abstractions;

namespace Domain.Hilos
{
    public class ComentarioDestacado : Entity<ComentarioDestacadoId>
    {
        public ComentarioId ComentarioId { get; private set; }
        public HiloId HiloId { get; private set; }

        public ComentarioDestacado(ComentarioId comentarioId, HiloId hiloId)
        {
            Id = new ComentarioDestacadoId(Guid.NewGuid());
            ComentarioId = comentarioId;
            HiloId = hiloId;
        }
    }

    public class ComentarioDestacadoId : EntityId
    {
        public ComentarioDestacadoId(Guid id) : base(id) { }
    }
}