
using Domain.Usuarios;

namespace Application.Abstractions
{
    public interface IUserContext
    {
        bool IsLogged { get; }
        Guid UsuarioId { get; }
        Rango Rango { get; }
        string Username { get; }
        string Moderador { get; }
    }
}

