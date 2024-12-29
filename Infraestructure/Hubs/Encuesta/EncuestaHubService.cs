using Application.Encuestas.Abstractions;
using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Encuestas;

public class EncuestaHubService : IEncuestasHubService
{

    private readonly IHubContext<EncuestaHub, IEncuestasHub> _hub;

    public EncuestaHubService(IHubContext<EncuestaHub, IEncuestasHub> hub)
    {
        _hub = hub;
    }

    public async Task NotificarEncuestaVotada(Guid encuesta, Guid respuesta) =>await  _hub.Clients.Group(encuesta.ToString()).OnEncuestaVotada(respuesta);
}