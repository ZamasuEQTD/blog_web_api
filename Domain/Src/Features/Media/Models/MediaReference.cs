using SharedKernel.Abstractions;

namespace Domain.Media {
    public class MediaReference : Entity<MediaReferenceId>
    {
        public bool Spoiler { get; private set; }
        public  MediaSourceId MediaId { get; private set; }

        private MediaReference() : base() {}
        public MediaReference(MediaReferenceId id, MediaSourceId mediaId, bool spoiler) : base(id)
        {
            // MediaId = mediaId;
            Spoiler = spoiler;
        }

        
    }
        public class MediaReferenceId : EntityId {
        public MediaReferenceId() : base(){}

            public MediaReferenceId(Guid id):base(id){
                
            }
        }
}