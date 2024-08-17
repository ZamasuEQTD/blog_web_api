using Domain.Hilos;
using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Denuncias
{
    public abstract class Denuncia : Entity<DenunciaId>
    {
        public UsuarioId DenuncianteId { get; private set; }
        public DenunciaStatus Status { get; private set; }

        protected Denuncia() { }
        protected Denuncia(UsuarioId denuncianteId)
        {
            this.Id = new(Guid.NewGuid());
            this.Status = DenunciaStatus.Activa;
            this.DenuncianteId = denuncianteId;
        }

        public void Desestimar()
        {
            this.Status = DenunciaStatus.Destimada;
        }

        public enum DenunciaStatus
        {
            Activa,
            Destimada
        }
    }

    public class DenunciaId : EntityId
    {
        private DenunciaId() : base() { }

        public DenunciaId(Guid id) : base(id) { }
    }
}