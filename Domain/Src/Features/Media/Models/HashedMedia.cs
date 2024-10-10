using Domain.Media.ValueObjects;
using SharedKernel.Abstractions;

namespace Domain.Media
{
    public class HashedMedia : Entity<MediaId>
    {
        public string Hash { get; private set; }
        public string Path { get; private set; }
        protected HashedMedia() { }
        protected HashedMedia(string hash, string path)
        {
            Id = new MediaId(Guid.NewGuid());
            Hash = hash;
            Path = path;
        }
    }
}