using Domain.Baneos;
using Domain.Comentarios;
using SharedKernel.Abstractions;

namespace Domain.Usuarios{
    public abstract class Usuario : Entity<UsuarioId> {
        public List<Notificacion> Notificaciones { get; private set; }
        public Username Username { get; private set; }
        public string HashedPassword { get; private set; }
        public RangoDeUsuario Rango { get; set; }
        protected Usuario(){}
        protected Usuario(
            Username username,
            string HashedPassword
        ) : base() {
            this.Username = username;
            this.HashedPassword = HashedPassword;
            this.Notificaciones = [];
        }

        public void Notificar(Notificacion notificacion){
            Notificaciones.Add(notificacion);
        }

        public void LeerNotificacion(NotificacionId id) {
            Notificacion notificacion = Notificaciones.Single(n=> n.Id == id);

            notificacion.Leer();
        }
        public void LeerTodasLasNotificaciones(){
            foreach (var notificacion in Notificaciones)
            {
                notificacion.Leer();
            }
        }

        public enum RangoDeUsuario {
            Anonimo,
            Moderador
        }
    }

    public class Anonimo : Usuario {
        public List<Baneo> Baneos { get; private set; }

        private Anonimo(){}

        public Anonimo(
            Username username,
            string hashedPassword
        ):base(username,hashedPassword) {
            this.Id = new (Guid.NewGuid());
            this.Rango = RangoDeUsuario.Anonimo;
            this.Baneos = [];
        }  

        public void Banear(UsuarioId moderador, string mensaje){
            Baneos.Add(new Baneo(
                moderador,
                Id,
                mensaje
            ));
        }

        public void Desbanear(){
            foreach (var baneo in Baneos)
            {
                baneo.Eliminar();
            }
        }
    }

    public class Moderador : Usuario {
        public Moderador(UsuarioId id, Username username, string HashedPassword) : base(username, HashedPassword) {
            this.Id = id;
            this.Rango = RangoDeUsuario.Moderador;
        }        
    }
}