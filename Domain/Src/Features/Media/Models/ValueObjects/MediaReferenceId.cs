using SharedKernel.Abstractions;

namespace Domain.Media.ValueObjects
{
    public class MediaReferenceId : EntityId
    {
        private MediaReferenceId() { }

        public MediaReferenceId(Guid id) : base(id) { }
    }
}