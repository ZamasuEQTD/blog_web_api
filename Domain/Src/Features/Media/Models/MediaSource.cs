using SharedKernel.Abstractions;

namespace Domain.Media
{
    public abstract class MediaSource : Entity<MediaSourceId> {
        public MediaId Media {get;private set;}
        protected MediaSource() {}
        protected MediaSource(MediaSourceId id, Media media) {
            this.Id = id;
            this.Media = media.Id;
        }
    }

    
    public class HashedMedia : MediaSource  {
        public string Hash {get;private set;}
        private HashedMedia() : base() {}
        public HashedMedia(MediaSourceId id, Media media, string hash) : base(id, media)
        {
            Hash = hash;
        }

    }

    public class NonHashedMedia : MediaSource
    {
        public NonHashedMedia(MediaSourceId id, Media media) : base(id, media) {
        }
    }

    public class MediaSourceId : EntityId {
        public MediaSourceId():base(){}
        public MediaSourceId(Guid id) : base(id) {}
    }

}