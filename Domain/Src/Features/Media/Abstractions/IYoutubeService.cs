namespace Domain.Media.Abstractions
{
    public interface IYoutubeService {
        public bool EsYoutubeValido(string url);
        public string GetVideoId(string url);
        public string GetYoutubeVideoPrevisualizador(string videoId);
    }
}