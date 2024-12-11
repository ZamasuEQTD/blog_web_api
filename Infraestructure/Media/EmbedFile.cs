using Application.Medias.Abstractions;

namespace Infraestructure.Media
{
    public class EmbedFile : IEmbedFile
    {
        static private readonly Dictionary<string, EmbedType> _domains =  new Dictionary<string, EmbedType> {
            { "youtube.com", EmbedType.Youtube },
            { "youtu.be", EmbedType.Youtube },
        };

        private readonly string _url;
        public string Url => _url;

        public string Domain =>  Uri.TryCreate(_url, UriKind.Absolute, out var uri) ? uri.Host : "";

        public EmbedType Type => _domains.GetValueOrDefault(Domain, EmbedType.Desconocido);

        public EmbedFile(string path)
        {
            _url = path;
        }
    }
}