using SharedKernel.Abstractions;

namespace Domain.Media {
    public class Media : Entity <MediaId> {
        public string Hash {get;private set;}

        public Media(MediaId id, string hash ) : base(id)
        {
            Hash = hash;
        }
    }

    public class MediaId : EntityId {
            public MediaId(Guid id) : base(id){}
    } 
}