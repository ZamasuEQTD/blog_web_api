using Domain.Baneos;
using Domain.Comentarios;
using Domain.Notificaciones;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public abstract class Usuario : Entity<UsuarioId>
    {
        public Username Username { get; private set; }
        public string HashedPassword { get; private set; }
        public RangoDeUsuario Rango { get; set; }

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

    }

    public class Moderador : Usuario
    {
        public Moderador(UsuarioId id, Username username, string HashedPassword) : base(username, HashedPassword)
        {
            this.Id = id;
            this.Rango = RangoDeUsuario.Moderador;
        }
    }

    public class RangoDeUsuario : ValueObject
    {
        public string Rango { get; set; }
        public string RangoCorto { get; set; }

        private RangoDeUsuario() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Rango, RangoCorto };
        }

        public static RangoDeUsuario Anonimo = new RangoDeUsuario { Rango = "Anonimo", RangoCorto = "ANON" };
        public static RangoDeUsuario Moderador = new RangoDeUsuario { Rango = "Moderador", RangoCorto = "MOD" };
        public static RangoDeUsuario Owner = new RangoDeUsuario { Rango = "Owner", RangoCorto = "MOD" };
    }
}