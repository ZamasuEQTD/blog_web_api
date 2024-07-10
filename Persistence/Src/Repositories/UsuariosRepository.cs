using Domain.Usuarios;
using Domain.Usuarios.Abstractions;

namespace Persistence.Repositories
{
    public class UsuariosRepository : IUsuariosRepository
    {
        public void Add(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetUsuarioById(UsuarioId id)
        {
            throw new NotImplementedException();
        }

        public Task<Usuario?> GetUsuarioByUsername(Username username)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UsernameEstaOcupado(Username username)
        {
            throw new NotImplementedException();
        }
    }
}