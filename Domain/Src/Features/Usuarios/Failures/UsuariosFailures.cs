
using SharedKernel;

namespace Domain.Usuarios.Failures
{
    static public class UsuariosFailures
    {
        public static readonly Error USERNAME_OCUPADO = new Error("Usuarios.UsernameOcupado");
        public static readonly Error USERNAME_O_PASSWORD_INVALIDA = new Error("Usuarios");

    }
}