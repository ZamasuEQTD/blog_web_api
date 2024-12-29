namespace Infraestructure.Hubs.Encuestas;

public interface IEncuestasHub {
    Task OnEncuestaVotada(Guid respuesta);
}