using Infraestructure.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Hubs.Hilo;

public class HilosHub : Hub<IHilosHub>
{
    private readonly IServiceProvider _services;
    private readonly HilosConnectionsContext _connections;

    public HilosHub(IServiceProvider services, HilosConnectionsContext connections)
    {
        _services = services;
        _connections = connections;
    }

    public Task SubscribirseHilo(Guid hiloId)
    {
        IUserHubContext user = _services.GetService<IUserHubContext>()!;

        if (!_connections.Connections.ContainsKey(hiloId))
        {
            _connections.Connections.Add(hiloId, new HashSet<IUserHubContext>(){
                user
            });
        } else {
            _connections.Connections[hiloId].Add(user);
        }

        return Task.CompletedTask;
    }

    public Task DesubscribirseHilo(Guid hiloId)
    {
        IUserHubContext user = _services.GetService<IUserHubContext>()!;

        if (_connections.Connections.ContainsKey(hiloId))
        {
            _connections.Connections[hiloId].Remove(user);
        }

        return Task.CompletedTask;
    }

    public override Task OnDisconnectedAsync(Exception? exception)
    {
        IUserHubContext user = _services.GetService<IUserHubContext>()!;

        foreach (var connection in _connections.Connections)
        {
            connection.Value.Remove(user);
        }

        return base.OnDisconnectedAsync(exception);
    }
}