using Domain.Usuarios;

namespace Domain.Usuarios.Abstractions
{
    public interface IUsuariosRepository {
        public Task<Usuario?> GetUsuarioById(UsuarioId id);
        public Task<Usuario?> GetUsuarioByUsername(Username username);
        public Task<bool> UsernameEstaOcupado(Username username);
        public void Add(Usuario usuario);
    }
}