namespace Application.Encuestas.Abstractions;

public interface IEncuestasHubService
{
    public Task NotificarEncuestaVotada(Guid encuesta, Guid respuesta);
}