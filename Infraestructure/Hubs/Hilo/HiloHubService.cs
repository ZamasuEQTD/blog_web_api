using Application.Comentarios.GetComentarioDeHilos;
using Infraestructure.Hubs.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Hilo;

public interface IHilosHubService {
    Task NotificarEncuestaVotada(Guid respuesta);
    Task NotificarHiloComentado(GetComentarioResponse comentario);
    Task NotificarComentarioEliminado(Guid id);
}

public class HilosConnectionsContext
{
    public Dictionary<Guid, HashSet<IUserHubContext>> Connections { get; set; } = new();
}

public class HiloHubService : IHilosHubService
{

    private readonly IHubContext<HilosHub, IHilosHub> _hub;
    public Task NotificarComentarioEliminado(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task NotificarEncuestaVotada(Guid respuesta)
    {
        throw new NotImplementedException();
    }

    public Task NotificarHiloComentado(GetComentarioResponse comentario)
    {
        throw new NotImplementedException();
    }
}