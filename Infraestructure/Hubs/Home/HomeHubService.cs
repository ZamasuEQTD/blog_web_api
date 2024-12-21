
using Application.Hilos;
using Application.Hilos.Queries.Responses;
using Infraestructure.Hubs.Abstractions;
using Infraestructure.Hubs.Mappers;
using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Home;


public class HomeConnectionManager
{
    public Dictionary<string, IUserHubContext> Connections { get; set; } = new Dictionary<string, IUserHubContext>();
}

public class HomeHubService : IHomeHubService
{
    private readonly HomeConnectionManager _connectionManager;
    private readonly IHubContext<HomeHub, IHomeHub> _hub;

    public HomeHubService(IHubContext<HomeHub, IHomeHub> hub, HomeConnectionManager connectionManager)
    {
        _hub = hub;
        _connectionManager = connectionManager;
    }

    public async Task NotificarHiloEliminado(Guid id) => await _hub.Clients.All.OnHiloEliminado(id);
    public Task NotificarHiloPosteado(GetHiloPortadaResponse portada)
    {

        List<Task> tasks = new List<Task>();

        foreach (var connection in _connectionManager.Connections.Values)
        {
            GetHiloPortadaResponse response;

            if (connection.IsAuthenticated && connection.UserId == portada.AutorId)
                response = PortadaHubMapper.ToAutor(portada);
            else if (connection.IsAuthenticated && connection.Role == UserType.Moderador)
                response = PortadaHubMapper.ToModerador(portada);
            else
                response = PortadaHubMapper.ToAnonimo(portada);

            tasks.Add(_hub.Clients.Client(connection.ConnectionId).OnHiloPosteado(response));
        }

        return Task.WhenAll(tasks);
    }
}