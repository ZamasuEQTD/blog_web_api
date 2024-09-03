using Domain.Baneos;
using Domain.Comentarios;
using Domain.Notificaciones;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public abstract class Usuario : Entity<UsuarioId>
    {
        public Username Username { get; private set; }
        public string HashedPassword { get; private set; }
        public RangoDeUsuario Rango { get; set; }
        public List<Notificacion> Notificaciones { get; private set; } = [];

        protected Usuario(
            Username username,
            string HashedPassword
        ) : base()
        {
            this.Username = username;
            this.HashedPassword = HashedPassword;
        }
        protected Usuario()
        {

        }

        public enum RangoDeUsuario
        {
            Anonimo,
            Moderador
        }

        public void Notificar(Notificacion notificacion)
        {
            Notificaciones.Add(notificacion);
        }
    }

    public class Anonimo : Usuario
    {

        private Anonimo() { }

        public Anonimo(
            Username username,
            string hashedPassword
        ) : base(username, hashedPassword)
        {
            Id = new(Guid.NewGuid());
            Rango = RangoDeUsuario.Anonimo;
        }

        public Baneo Banear(UsuarioId moderador, string mensaje)
        {
            return new Baneo(
                moderador,
                Id,
                mensaje
            );
        }
    }

    public class Moderador : Usuario
    {
        public Moderador(UsuarioId id, Username username, string HashedPassword) : base(username, HashedPassword)
        {
            this.Id = id;
            this.Rango = RangoDeUsuario.Moderador;
        }
    }
}