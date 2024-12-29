using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hubs.Encuestas;

public class EncuestaHub : Hub<IEncuestasHub> {

    public async Task SubscribirseEncuesta(Guid encuesta) => await  Groups.AddToGroupAsync(this.Context.ConnectionId, encuesta.ToString());
}