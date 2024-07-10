using Application.Abstractions;
using Domain.Usuarios;
using Microsoft.AspNetCore.Http;

namespace Infraestructure
{
    public class UserContext(IHttpContextAccessor context) : IUserContext
    {
        public bool IsLogged => true;
        public Guid UsuarioId => Guid.NewGuid();
        public Usuario.RangoDeUsuario Rango => Usuario.RangoDeUsuario.Moderador;
    }
}