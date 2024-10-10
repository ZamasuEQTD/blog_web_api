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
        public string Miniatura { get; private set; }
        public string Previsulizacion { get; private set; }
        public YoutubeVideo()
        {

        }
        public YoutubeVideo(string hash, string path, string miniatura, string previsulizacion) : base(hash, path, NetworkSource.Youtube)
        {
            Miniatura = miniatura;
            Previsulizacion = previsulizacion;
        }
    }
}