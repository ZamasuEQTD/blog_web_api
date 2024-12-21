using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Hilos.Events;

public class HiloEliminadoEventHandler : INotificationHandler<HiloEliminadoDomainEvent>
{
    private readonly IHomeHubService _hub;

    public HiloEliminadoEventHandler(IHomeHubService hub)
    {
        _hub = hub;
    }

    public async Task Handle(HiloEliminadoDomainEvent notification, CancellationToken cancellationToken)
    {
        await _hub.NotificarHiloEliminado(notification.HiloId.Value);
    }
}