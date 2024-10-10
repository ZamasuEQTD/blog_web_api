using Domain.Media.Abstractions;

namespace Infraestructure.Media
{
    public class EmbedFile : IEmbedFile
    {
        private readonly string _url;
        public string Url => _url;
        public EmbedFile(string path)
        {
            _url = path;
        }
    }
}