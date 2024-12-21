using Application.Hilos.Queries.Responses;

namespace Infraestructure.Hubs.Home;

public interface IHomeHub
{
    Task OnHiloEliminado(Guid id);
    Task OnHiloPosteado(GetHiloPortadaResponse portada);
}