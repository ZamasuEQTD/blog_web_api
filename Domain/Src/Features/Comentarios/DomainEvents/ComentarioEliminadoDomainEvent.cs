using Domain.Hilos;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.DomainEvents;

public class ComentarioEliminadoDomainEvent : IDomainEvent
{
    public HiloId HiloId {get;set;}

    public ComentarioId ComentarioId {get;private set;}
    public ComentarioEliminadoDomainEvent(HiloId hiloId, ComentarioId comentarioId)
    {
        HiloId = hiloId;
        ComentarioId = comentarioId;
    }


}