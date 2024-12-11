using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
        public Guid UsuarioId => Guid.Parse(context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)!.Value);
        public string Username =>   context.HttpContext!.User.Identity!.Name!;
        public string Moderador => context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == "moderador")!.Value;
        public Autor Autor =>  new Autor("Anonimo", "ANON");
    }
}