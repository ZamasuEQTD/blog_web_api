using Domain.Hilos;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Stickies
{
    public class Sticky : Entity<StickyId>
    {
        public HiloId Hilo { get; private set; }

        public Sticky(HiloId hilo )
        {
            this.Id = new(Guid.NewGuid());
            this.Hilo = hilo;
        }
    }

    public class StickyId : EntityId
    {
        public StickyId(Guid id) : base(id) { }
    }
}