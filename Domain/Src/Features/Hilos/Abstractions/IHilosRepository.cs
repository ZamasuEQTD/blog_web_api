using Domain.Stickies;
using Domain.Usuarios;

namespace Domain.Hilos.Abstractions
{
    public interface IHilosRepository
    {
        void Add(Hilo hilo);
        void Add(DenunciaDeHilo denuncia);
        Task<Hilo?> GetHiloById(HiloId id);
        Task<bool> TieneStickyActivo(HiloId hiloId, DateTime now);
        Task<bool> HaDenunciado(HiloId hiloId, UsuarioId usuarioId);
        Task<List<DenunciaDeHilo>> GetDenuncias(HiloId id);
        Task<Sticky?> GetStickyActivo(HiloId id, DateTime now);
    }
}