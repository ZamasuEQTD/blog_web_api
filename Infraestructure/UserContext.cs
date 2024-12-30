using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Application.Abstractions;
using Domain.Usuarios;
using Microsoft.AspNetCore.Http;

namespace Infraestructure
{
    public class UserContext(IHttpContextAccessor context) : IUserContext
    {
        public bool IsAuthenticated =>
        context
            .HttpContext?
            .User
            .Identity?
            .IsAuthenticated ??
        throw new ApplicationException("User context is unavailable");
        
        public Guid UsuarioId => Guid.Parse(context.HttpContext!.User.Claims.FirstOrDefault(s => s.Type == ClaimTypes.NameIdentifier)!.Value);
        public string Username =>   context.HttpContext!.User.Identity!.Name!;
        public List<string> Roles => context.HttpContext!.User.Claims.Where( s=> s.Type == "role").Select(c=> c.Value).ToList() ;
        public Autor Autor => new Autor(Roles.Contains(Role.Moderador.Name!)? Username : "Anonimo", Roles.Select(x=>x != Role.Anonimo.Name).Any()? "Mod" : "Anon");
    }
}