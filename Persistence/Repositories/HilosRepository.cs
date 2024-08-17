using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Stickies;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class HilosRepository : IHilosRepository
    {
        private readonly BlogDbContext _context;
        public HilosRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Hilo hilo) => _context.Add(hilo);

        public void Add(DenunciaDeHilo denuncia) => _context.Add(denuncia);

        public Task<List<DenunciaDeHilo>> GetDenuncias(HiloId id) => _context.DenunciasDeHilo.Where(d => d.HiloId == id).ToListAsync();
        public Task<Hilo?> GetHiloById(HiloId id) => _context.Hilos.FirstOrDefaultAsync(h => h.Id == id);
        public Task<Sticky?> GetStickyActivo(HiloId id, DateTime now) => _context.Stickies.FirstOrDefaultAsync(s => s.Activo(now));
        public Task<bool> HaDenunciado(HiloId hiloId, UsuarioId usuarioId) => _context.DenunciasDeHilo.AnyAsync(d => d.HiloId == hiloId && d.DenuncianteId == usuarioId);
        public Task<bool> TieneStickyActivo(HiloId hiloId, DateTime now) => _context.Stickies.AnyAsync(s => s.Hilo == hiloId && s.Activo(now));
    }
}