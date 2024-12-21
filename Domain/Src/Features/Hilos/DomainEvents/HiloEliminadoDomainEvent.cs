using SharedKernel.Abstractions;

namespace Domain.Hilos.DomainEvents;

public class HiloEliminadoDomainEvent : IDomainEvent
{
    public HiloId HiloId {get;private  set;}

    public HiloEliminadoDomainEvent(HiloId hiloId)
    {
        HiloId = hiloId;
    }
}