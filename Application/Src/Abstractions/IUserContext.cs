
using Domain.Usuarios;

namespace Application.Abstractions
{
    public interface IUserContext
    {
        bool IsLogged { get; }
        Guid UsuarioId { get; }
        Usuario.RangoDeUsuario Rango { get; }
    }
}

