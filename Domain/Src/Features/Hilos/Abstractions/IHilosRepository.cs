using Domain.Comentarios;
using Domain.Usuarios;

namespace Domain.Hilos.Abstractions
{
    public interface IHilosRepository
    {
        void Add(Hilo hilo);
        void Add(RelacionDeHilo relacion);

        Task<Hilo?> GetHiloById(HiloId id);
        Task<RelacionDeHilo?> GetRelacion(HiloId hiloId, UsuarioId id);
        void Add(Comentario comentario);
    }
}