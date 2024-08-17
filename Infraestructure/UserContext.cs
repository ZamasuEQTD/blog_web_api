using Application.Abstractions;
using Domain.Usuarios;
using Microsoft.AspNetCore.Http;

namespace Infraestructure
{
    public class UserContext(IHttpContextAccessor context) : IUserContext
    {
        public bool IsLogged =>
        context
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");

        public Guid UsuarioId => Guid.Parse(context.HttpContext.User.Claims.FirstOrDefault(s => s.Type == "UserId").Value);

        public Usuario.RangoDeUsuario Rango => Usuario.RangoDeUsuario.Moderador;
    }
}