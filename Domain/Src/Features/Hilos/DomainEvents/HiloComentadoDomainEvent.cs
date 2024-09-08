using Domain.Comentarios;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Events
{
    public class HiloComentadoDomainEvent : IDomainEvent
    {
        public HiloId HiloId { get; set; }
        public ComentarioId ComentarioId { get; set; }
        public HiloComentadoDomainEvent(HiloId hiloId, ComentarioId comentarioId)
        {
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }
    }
}