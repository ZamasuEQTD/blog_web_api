using Domain.Comentarios;
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
        public void Add(Comentario comentario) => _context.Add(comentario);
        public void Add(HiloInteraccion relacion) => _context.Add(relacion);
        public Task<Hilo?> GetHiloById(HiloId id) => _context.Hilos.Include(h => h.Notificaciones).Include(h=> h.Comentarios).FirstOrDefaultAsync(h => h.Id == id);
        public Task<HiloInteraccion?> GetRelacion(UsuarioId usuarioId) => _context.Relaciones.FirstOrDefaultAsync(r => r.UsuarioId == usuarioId);
        public Task<HiloInteraccion?> GetRelacion(HiloId hiloId, UsuarioId usuarioId) => _context.Relaciones.FirstOrDefaultAsync(r => r.HiloId == hiloId && r.UsuarioId == usuarioId);
    }
}