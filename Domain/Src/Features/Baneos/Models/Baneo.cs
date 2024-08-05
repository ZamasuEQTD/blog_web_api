using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Baneos
{
    public class Baneo : Entity<BaneoId> {
        public UsuarioId ModeradorId {get;private set;}
        public UsuarioId UsuarioBaneadoId {get;private set;}
        public DateTime Concluye { get; private set;}        
        public BaneoStatus Status { get; private set;}
        public bool Activo(DateTime utcNow) => Status == BaneoStatus.Activo && utcNow < Concluye;
        public string Mensaje { get; private set; }

        public Baneo(UsuarioId moderadorId, UsuarioId usuarioBaneadoId, string mensaje) {
            this.Id = new BaneoId(Guid.NewGuid());
            this.ModeradorId = moderadorId;
            this.UsuarioBaneadoId = usuarioBaneadoId;
            this.Mensaje = mensaje;
            this.Status = BaneoStatus.Activo;
        }

        public void Eliminar(){
            Status = BaneoStatus.Eliminado;
        }

        public enum BaneoStatus
        {
            Activo,
            Eliminado
        }
    }

    public class BaneoId : EntityId
    {
        public BaneoId(Guid id) : base(id) { }
    }
}