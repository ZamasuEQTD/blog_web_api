using Application.Hilos.Queries.Responses;

namespace Application.Hilos;
public interface IHomeHubService {
    Task NotificarHiloPosteado(GetHiloPortadaResponse portada); 
    Task NotificarHiloEliminado(Guid id);
}