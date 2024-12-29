using Application.Comentarios.GetComentarioDeHilos;
using Application.Features.Hilos.Abstractions;
using Infraestructure.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Hilo;

public class HilosConnectionsContext
{
    public Dictionary<Guid, HashSet<IUserHubContext>> Connections { get; set; } = new();
}

public class HilosHubService : IHilosHubService
{

    private readonly IHubContext<HilosHub, IHilosHub> _hub;
    private readonly HilosConnectionsContext _connections;
    public HilosHubService(
        IHubContext<HilosHub, IHilosHub> hub,
        HilosConnectionsContext connections
        )
    {
        _hub = hub;
        _connections = connections;
    }

    public async Task NotificarComentarioEliminado(Guid hilo, Guid comentario) => await this._hub.Clients.Group(hilo.ToString()).OnComentarioEliminado(comentario);

    public async Task NotificarHiloComentado(Guid hilo, GetComentarioResponse comentario)
    {
        _connections.Connections.TryGetValue(hilo, out var usuarios);

        if(usuarios is null) return;

        foreach (var usuario in usuarios)
        {
            await _hub.Clients.Client(usuario.ConnectionId).OnHiloComentado(comentario);
        }
    }
}