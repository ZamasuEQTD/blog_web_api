using Application.Abstractions;
using Microsoft.Extensions.Configuration;

namespace Infraestructure
{
    public class UrlConfigurationProvider : IUrlProvider
    {
        private readonly IConfiguration _configuration;
        public UrlConfigurationProvider(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public string BaseUrl => _configuration["Url"]!;
        public string Media => BaseUrl + "/media/";
        public string Files => BaseUrl + "/media/files/";

        public string Thumbnail => Media + "thumbnails/";
        public string Previsualizacion =>  Media + "previsualizaciones/";
    }
}