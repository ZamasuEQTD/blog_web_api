using SharedKernel;

namespace Domain.Usuarios.Failures
{
    static public class PasswordFailures
    {
        static public readonly Error TIENE_ESPACIOS_EN_BLANCO = new Error("Usuarios.PasswordTieneEspaciosEnBlanco");
        static public readonly Error LARGO_INVALIDO = new Error("Usuarios.PasswordLargoInvalido");

    }
}