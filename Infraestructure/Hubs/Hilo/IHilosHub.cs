using Application.Comentarios.GetComentarioDeHilos;

namespace Infraestructure.Hubs.Hilo
{
    public interface IHilosHub {
        Task OnEncuestaVotada(Guid respuesta);
        Task OnHiloComentado(GetComentarioResponse comentario);
        Task OnComentarioEliminado(Guid id);
    }
}