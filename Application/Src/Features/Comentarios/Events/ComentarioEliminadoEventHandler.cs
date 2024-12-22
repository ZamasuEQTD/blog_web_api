using Domain.Comentarios.DomainEvents;
using MediatR;

namespace Application.Comentarios.Events;

public class ComentarioEliminadoEventHandler : INotificationHandler<ComentarioEliminadoDomainEvent>
{
    public async Task Handle(ComentarioEliminadoDomainEvent notification, CancellationToken cancellationToken)
    {
    }
}