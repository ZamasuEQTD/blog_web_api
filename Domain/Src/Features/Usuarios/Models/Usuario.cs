using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios
{
    public abstract class Usuario : Entity<UsuarioId> {
        public Username Username { get; private set; }
        public string HashedPassword { get; private set; }
        public RangoDeUsuario Rango { get; set; }
        protected Usuario(){}
        protected Usuario(
            UsuarioId id,
            Username username,
            string HashedPassword
        ) : base(id) {
            this.Username = username;
            this.HashedPassword = HashedPassword;
        }
        public enum RangoDeUsuario {
            Anonimo,
            Moderador
        }
    }

    public class Anonimo : Usuario {
        private Anonimo(){}
        private Anonimo(
            UsuarioId id,
            Username username,
            string hashedPassword
        ):base(id,username,hashedPassword) {}  

        static public Anonimo Create(
            UsuarioId id, 
            Username username,
            string hashedPassword
        ){
            return new Anonimo(
                id, 
                username, 
                hashedPassword
            );
        }

    }


    public class Moderador : Usuario {
        public Moderador(UsuarioId id, Username username, string HashedPassword) : base(id, username, HashedPassword) {}
    }
}