using Domain.Usuarios;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        public Task<string> Generar(Usuario usuario);

    }
}