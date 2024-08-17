using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {

        private readonly BlogDbContext _context;

        public UsuariosRepository(BlogDbContext context)
        {
            _context = context;
        }

        public void Add(Usuario usuario)
        {
            _context.Usuarios.Add(usuario);
        }

        public Task<Usuario?> GetUsuarioById(UsuarioId id)
        {
            return _context.Usuarios.FirstOrDefaultAsync(u => u.Id == id);
        }

        public Task<Usuario?> GetUsuarioByUsername(Username username)
        {
            return _context.Usuarios.FirstOrDefaultAsync(u => u.Username == username);
        }

        public Task<bool> UsernameEstaOcupado(Username username)
        {
            return _context.Usuarios.AnyAsync(u => u.Username == username);
        }
    }
}