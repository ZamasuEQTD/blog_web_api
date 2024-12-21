using Infraestructure.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.DependencyInjection;

namespace Infraestructure.Hubs.Home;
public class HomeHub : Hub<IHomeHub>
{
    private readonly HomeConnectionManager _connectionManager;
    public HomeHub(HomeConnectionManager connectionManager)
    {
        _connectionManager = connectionManager;
    }

    public override async Task OnConnectedAsync()
    {

        IUserHubContext usuario = new UserHubContext(Context);

        _connectionManager.Connections[Context.ConnectionId] = usuario;

        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        _connectionManager.Connections.Remove(Context.ConnectionId);

        await base.OnDisconnectedAsync(exception);
    }
}