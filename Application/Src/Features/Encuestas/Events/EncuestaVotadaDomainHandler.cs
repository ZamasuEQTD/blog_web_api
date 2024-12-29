using Application.Encuestas.Abstractions;
using Domain.Encuestas.DomainEvents;
using MediatR;

namespace Application.Encuestas.Events;

public class EncuestaVotadaDomainHandler : INotificationHandler<EncuestaVotadaDomainEvent>
{
    private readonly IEncuestasHubService _hub;

    public EncuestaVotadaDomainHandler(IEncuestasHubService hub)
    {
        this._hub = hub;
    }

    public  async Task Handle(EncuestaVotadaDomainEvent notification, CancellationToken cancellationToken)
    {
        await _hub.NotificarEncuestaVotada(notification.EncuestaId.Value, notification.RespuestaId.Value);
    }
}