
using SharedKernel.Abstractions;

namespace Domain.Medias {
    public class MediaReference : Entity<MediaReferenceId>
    {
        public bool Spoiler { get; private set; }
        public  HashedMediaProviderId ProviderId { get; private set; }

        private MediaReference() : base() {}
        public MediaReference(HashedMediaProviderId providerId, bool spoiler) : base()
        {
            Id = new (Guid.NewGuid());
            ProviderId = providerId;
            Spoiler = spoiler;
        }

        
    }
        public class MediaReferenceId : EntityId {
        public MediaReferenceId() : base(){}

            public MediaReferenceId(Guid id):base(id){
                
            }
        }
}