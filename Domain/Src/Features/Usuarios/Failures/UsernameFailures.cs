using SharedKernel;

namespace Domain.Usuarios.Failures
{
    public static class UsernameFailures
    {
        public static readonly Error TIENE_ESPACIOS_EN_BLANCO = new Error("Usuarios.UsernameInvalidoContieneEspaciosEnBlanco", "Tiene");
        public static readonly Error LARGO_INVALIDO = new Error("Usuarios.UsernameInvalidoLargoInvalido", $"El nombre de usuario debe tener entre {Username.MIN_LENGTH} y {Username.MAXIMO_LENGTH} caracteres");
    }
}