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
        public Rango Rango { get; set; }

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
            Rango = Rango.Anonimo;
        }

    }

    public class Moderador : Usuario
    {
        public string NombreModerador { get; set; }

        public Moderador(UsuarioId id, Username username, string HashedPassword) : base(username, HashedPassword)
        {
            this.Id = id;
            this.Rango = Rango.Moderador;
        }
    }

    public class RangoDeUsuario : ValueObject
    {
        public Rango Rango { get; set; }
        public string RangoCorto { get; set; }

        private RangoDeUsuario() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Rango, RangoCorto };
        }

        public static RangoDeUsuario Anonimo = new RangoDeUsuario { Rango = Rango.Anonimo, RangoCorto = "ANON" };
        public static RangoDeUsuario Moderador = new RangoDeUsuario { Rango = Rango.Moderador, RangoCorto = "MOD" };
        public static RangoDeUsuario Owner = new RangoDeUsuario { Rango = Rango.Owner, RangoCorto = "MOD" };
    }

    public class Rango : ValueObject
    {
        public string Nombre { get; set; }

        private Rango() { }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Nombre };
        }

        public static Rango Anonimo = new Rango { Nombre = "Anonimo" };
        public static Rango Moderador = new Rango { Nombre = "Moderador" };
        public static Rango Owner = new Rango { Nombre = "Owner" };


        public static Rango FromNombre(string nombre)
        {
            return nombre switch
            {
                "Anonimo" => Anonimo,
                "Moderador" => Moderador,
                "Owner" => Owner,
                _ => throw new ArgumentException("Rango no válido")
            };
        }
    }

    public class Autor : ValueObject
    {
        public string Nombre { get; set; }
        public RangoDeUsuario Rango { get; set; }

        private Autor() { }
        public Autor(string nombre, RangoDeUsuario rango)
        {
            Nombre = nombre;
            Rango = rango;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Nombre, Rango };
        }
    }
}