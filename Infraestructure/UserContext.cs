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
        public Guid UsuarioId => Guid.Parse(context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == "user_id")!.Value);
        public string Username => context.HttpContext!.User.Identity!.Name!;
        public Rango Rango => Rango.FromNombre(context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == "rango")!.Value);
        public string Moderador => context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == "moderador")!.Value;
    }
}