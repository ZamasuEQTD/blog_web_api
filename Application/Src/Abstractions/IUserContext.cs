
using Domain.Usuarios;

namespace Application.Abstractions
{
    public interface IUserContext
    {
        bool IsAuthenticated { get; }
        Guid UsuarioId { get; }
        string Username { get; }
        Autor Autor {get;}
        List<string> Roles {get;}
    }
}

