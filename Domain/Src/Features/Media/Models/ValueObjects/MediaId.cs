using SharedKernel.Abstractions;

namespace Domain.Media.ValueObjects
{
    public class MediaId : EntityId
    {
        private MediaId() { }
        public MediaId(Guid id) : base(id) { }
    }
}