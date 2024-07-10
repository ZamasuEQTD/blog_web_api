using SharedKernel.Abstractions;

namespace Domain.Media
{
    public class MediaReference : Entity<MediaReferenceId>
    {
        public bool Spoiler { get; private set; }
        public  MediaId MediaId { get; private set; }
        public MediaReference(MediaReferenceId id, MediaId mediaId, bool spoiler) : base(id)
        {
            MediaId = mediaId;
            Spoiler = spoiler;
        }

        
    }
    public class MediaReferenceId : EntityId
        {
            public MediaReferenceId(Guid id):base(id){
                
            }
        }
}