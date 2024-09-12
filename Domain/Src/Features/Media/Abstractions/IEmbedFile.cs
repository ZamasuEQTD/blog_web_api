using Domain.Media.ValueObjects;

namespace Domain.Media.Abstractions
{
    public interface IEmbedFile
    {
        public string Url { get; }
        public NetworkSource Source { get; }
    }
}