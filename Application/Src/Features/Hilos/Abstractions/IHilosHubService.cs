namespace Application.Features.Hilos.Abstractions;

public interface IHilosHubService
{
    Task NotificarHiloPosteado(Guid id);
}