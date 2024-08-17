using Domain.Hilos;
using SharedKernel.Abstractions;

namespace Domain.Stickies
{
    public class Sticky : Entity<StickyId>
    {
        public DateTime? Conluye { get; private set; }
        public HiloId Hilo { get; private set; }
        public StickyStatus Status { get; private set; }
        public bool Conluido(DateTime utcNow) => utcNow > Conluye;
        public bool Activo(DateTime utc) => Status == StickyStatus.Activo && !Conluido(utc);
        public Sticky(HiloId hilo, DateTime? Conluye)
        {
            this.Id = new(Guid.NewGuid());
            this.Hilo = hilo;
            this.Conluye = Conluye;
            this.Status = StickyStatus.Activo;
        }

        public void Eliminar()
        {
            this.Status = StickyStatus.Eliminado;
        }

        public enum StickyStatus
        {
            Activo,
            Eliminado
        }
    }

    public class StickyId : EntityId
    {
        public StickyId(Guid id) : base(id) { }
    }

}