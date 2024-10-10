using Domain.Media.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Media
{
    public class MediaReference : Entity<MediaReferenceId>
    {
        public MediaId MediaId { get; private set; }
        public bool Spoiler { get; private set; }
        protected MediaReference() { }
        public MediaReference(MediaId mediaId, bool spoiler)
        {
            Id = new(Guid.NewGuid());
            MediaId = mediaId;
            Spoiler = spoiler;
        }

    }


}
