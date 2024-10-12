using Domain.Media.ValueObjects;

namespace Domain.Media
{
    public abstract class NetworkMedia : HashedMedia
    {
        public NetworkSource Source { get; private set; }
        protected NetworkMedia() { }
        protected NetworkMedia(string hash, string path, NetworkSource source) : base(hash, path)
        {
            Source = source;
        }
    }

    public class YoutubeVideo : NetworkMedia
    {
        public YoutubeVideo()
        {

        }
        public YoutubeVideo(string hash, string path) : base(hash, path, NetworkSource.Youtube)
        {
        }
    }
}