using Application.Comentarios.GetComentarioDeHilos;

namespace Application.Features.Hilos.Abstractions;

public interface IHilosHubService {
    Task NotificarHiloEliminado(Guid id);
    Task NotificarHiloComentado(GetComentarioResponse comentario);
}