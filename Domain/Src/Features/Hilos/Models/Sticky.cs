using Domain.Hilos;
using SharedKernel.Abstractions;

namespace Domain.Stickies
{
    public class Sticky : Entity<StickyId>
    {
        public DateTime? Conluye { get; private set; }
        public HiloId Hilo { get; private set; }
        public bool Conluido(DateTime utcNow) => Conluye is not null && utcNow > Conluye;
        public Sticky(HiloId hilo, DateTime? Conluye)
        {
            this.Id = new(Guid.NewGuid());
            this.Hilo = hilo;
            this.Conluye = Conluye;
        }
        public void Eliminar(DateTime now)
        {
            this.Conluye = now;
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