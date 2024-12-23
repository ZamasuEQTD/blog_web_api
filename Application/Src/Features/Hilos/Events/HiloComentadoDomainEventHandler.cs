using Application.Abstractions.Data;
using Dapper;
using Domain.Hilos.Events;
using MediatR;

namespace Application.Hilos.Events;

public class HiloComentadoDomainEventHandler : INotificationHandler<HiloComentadoDomainEvent>
{
    private readonly IDBConnectionFactory _connection;
    
    public HiloComentadoDomainEventHandler(IDBConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task Handle(HiloComentadoDomainEvent notification, CancellationToken cancellationToken)
    {
        using var connection = _connection.CreateConnection();


    }
}