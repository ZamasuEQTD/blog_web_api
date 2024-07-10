using Domain.Usuarios;

namespace Application.Abstractions
{
    public interface IJwtProvider
    {
        public string Generar(Usuario usuario);

    }
}