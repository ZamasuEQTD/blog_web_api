using SharedKernel;

namespace Domain.Usuarios
{
    public static class UsuariosFailures
    {
        public static readonly Error UsernameOcupado = new Error("Usuarios.UsernameOcupado", "El nombre de usuario ya está en uso.");
        public static readonly Error UsernameTieneEspaciosEnBlanco = new Error("Usuarios.UsernameTieneEspaciosEnBlanco", "El nombre de usuario no puede contener espacios en blanco.");
        public static readonly Error LongitudDeUsernameInvalida = new Error("Usuarios.LongitudDeUsernameInvalida", "La longitud del nombre de usuario es inválida.");
        public static readonly Error LongitudDePasswordInvalida = new Error("Usuarios.LongitudDePasswordInvalida", "La longitud de la contraseña es inválida.");
        public static readonly Error PasswordTieneEspaciosEnBlanco = new Error("Usuarios.PasswordTieneEspaciosEnBlanco", "La contraseña no puede contener espacios en blanco.");
        public static readonly Error UsernameOrPasswordIncorrecta = new Error("Usuarios.UsernameOrPasswordIncorrecta", "El nombre de usuario o la contraseña son incorrectos.");
    }
}