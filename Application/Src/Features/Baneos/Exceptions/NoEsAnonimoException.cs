
namespace Application.Usuarios.Exceptions
{
    public class NoEsUsuarioAnonimoException : InvalidCommandException
    {
        public NoEsUsuarioAnonimoException() : base(["No es un usuario anonimo"])
        {
        }
    }
}