using Microsoft.Extensions.FileProviders;
using Application.Medias.Services;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Media
{
    public class FormFileProvider : Application.Medias.Services.IFileProvider
    {
        private readonly IFormFile _file;
        private readonly bool _spoiler;
        public FormFileProvider(IFormFile file, bool spoiler)
        {
            _file = file;
            _spoiler = spoiler;
        }

        public string Extension => Path.GetExtension(FileName);

        public string FileName => _file.FileName;

        public string MimeType => throw new NotImplementedException();

        public Stream Stream => _file.OpenReadStream();

        public MediaType Media => throw new NotImplementedException();

        public bool Spoiler => _spoiler;
    }
}