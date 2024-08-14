using Application.Medias.Services;

namespace Infraestructure.Media
{
    public class UrlProvider : IUrlProvider
    {
        private readonly string _url;

        public UrlProvider(string url)
        {
            _url = url;
        }

        public string Url => _url;

        public UrlTypeProvider Provider => throw new NotImplementedException();

        public bool Spoiler => throw new NotImplementedException();
    }
}