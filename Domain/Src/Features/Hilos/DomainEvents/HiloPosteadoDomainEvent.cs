using SharedKernel.Abstractions;

namespace  Domain.Hilos.DomainEvents;

public class HiloPosteadoDomainEvent : IDomainEvent
{
    public HiloId HiloId {get; private set;}

    public HiloPosteadoDomainEvent(HiloId hiloId)
    {
        HiloId = hiloId;
    }
}