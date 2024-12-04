using Domain.Comentarios;
using Domain.Usuarios;

namespace Domain.Hilos.Abstractions
{
    public interface IHilosRepository
    {
        void Add(Hilo hilo);
        void Add(HiloInteraccion relacion);
        Task<Hilo?> GetHiloById(HiloId id);
        Task<HiloInteraccion?> GetRelacion(HiloId hiloId, UsuarioId id);
        void Add(Comentario comentario);
    }
}