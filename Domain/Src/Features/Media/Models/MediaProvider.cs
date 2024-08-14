using SharedKernel.Abstractions;

namespace Domain.Medias
{
    public class HashedMediaProvider : Entity<HashedMediaProviderId> {
        public MediaId Media { get; private set; }
        public string Hash { get; private set; }

        public HashedMediaProvider(string hash, MediaId media)
        {
            Id = new (Guid.NewGuid());
            Hash = hash;
            Media = media;
        }
    }

    public class HashedMediaProviderId : EntityId
    {
        public HashedMediaProviderId(Guid id) : base(id) { }
    }
}