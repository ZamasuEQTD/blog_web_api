using Domain.Baneos;
using Domain.Comentarios;
using Domain.Notificaciones;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public  class Usuario : Entity<UsuarioId>
    {
        public Username Username { get; private set; }
        public string HashedPassword { get; private set; }
        public Usuario(
            Username username,
            string HashedPassword
        ) : base()
        {
            this.Id = new UsuarioId(Guid.NewGuid());
            this.Username = username;
            this.HashedPassword = HashedPassword;
        }
        protected Usuario() {}
    }

    public class Autor
    {
        public string Nombre { get; set; }
        public string Rango { get; set; }	

        private Autor() {}
        public Autor(string nombre, string rango)
        {
            this.Nombre = nombre;
            this.Rango = rango;
        }
    }
}