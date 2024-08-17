using Domain.Baneos;
using Domain.Baneos.Abstractions;
using Domain.Usuarios;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class BaneosRepository : IBaneosRepository
    {
        private readonly BlogDbContext _context;

        public BaneosRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Baneo baneo) => _context.Add(baneo);
        public Task<Baneo> GetBaneoById(BaneoId id) => _context.Baneos.FirstAsync(b => b.Id == id);
        public Task<List<Baneo>> GetBaneos(UsuarioId usuarioId) => _context.Baneos.Where(b => b.UsuarioBaneadoId == usuarioId).ToListAsync();
    }
}