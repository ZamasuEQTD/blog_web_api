using Domain.Media.Services;
using Domain.Media.ValueObjects;

namespace Domain.Media.Abstractions
{
    public interface IEmbedFile
    {
        static private readonly List<IEmbedVerificador> enlaces = [
            new YoutubeEnlaceVerificador()
        ];

        public string Url { get; }
        public NetworkSource Source => enlaces.Generar(Url);
    }

    public interface IEmbedVerificador
    {
        NetworkSource? Verificar(string url);
    }

    public class YoutubeEnlaceVerificador : IEmbedVerificador
    {
        public NetworkSource? Verificar(string url)
        {
            if (YoutubeService.IsShortOrVideo(url)) return NetworkSource.Youtube;

            return null;
        }
    }


    static public class EnlaceExtensions
    {
        static public NetworkSource Generar(this List<IEmbedVerificador> verificadors, string url)
        {
            foreach (var item in verificadors)
            {
                NetworkSource? source = item.Verificar(url);

                if (source is not null) return source!;
            }

            return NetworkSource.Desconocido;
        }
    }
}