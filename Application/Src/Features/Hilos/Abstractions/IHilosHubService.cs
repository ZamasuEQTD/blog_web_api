using Application.Comentarios.GetComentarioDeHilos;

namespace Application.Features.Hilos.Abstractions;

public interface IHilosHubService {
    Task NotificarComentarioEliminado(Guid hilo,  Guid comentario);
    Task NotificarHiloComentado(Guid hilo,GetComentarioResponse comentario);
}