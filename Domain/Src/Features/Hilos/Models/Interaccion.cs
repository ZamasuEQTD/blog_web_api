using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos {
    public class Interaccion : Entity<InteraccionId> {
        public HiloId HiloId {get; private set;}
        public UsuarioId UsuarioId {get; private set;}	
        public bool Seguido {get; private set;}
        public bool Favorito {get; private set;}
        public bool Oculto {get; private set;}
        
        public Interaccion(HiloId hiloId, UsuarioId usuarioId){
            this.Id = new(Guid.NewGuid());
            this.HiloId = hiloId;
            this.UsuarioId = usuarioId;
            this.Seguido = false;
            this.Favorito = false;
            this.Oculto = false;
        }

        public void Seguir() {
            this.Seguido = !Seguido;
        }
        public void Ocultar() {
            this.Oculto = !Oculto;
        }
        public void PonerEnFavoritos() {
            this.Favorito = !Favorito;
        }

        public void EjecutarAccion(Acciones accion){
            switch (accion)
            {
                case Acciones.Seguir:
                    Seguir();
                    break;
                case Acciones.Favorito:
                    PonerEnFavoritos();
                    break;
                case Acciones.Ocultar:
                    Ocultar();
                    break;
                default:
                    throw new ArgumentException("Accion invalida");
            }
        }

        public enum Acciones {
            Seguir,
            Ocultar,
            Favorito
        }
    }

     

    public class InteraccionId : EntityId {
        public InteraccionId(Guid id) : base(id){}
    }
}