using Domain.Media.Abstractions;
using Domain.Media.ValueObjects;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Media
{
    public class FormFileImplementation(IFormFile file) : IFile
    {
        private static readonly Dictionary<string, FileType> types = new Dictionary<string, FileType>(){
            { "image", FileType.Imagen},
            { "video", FileType.Video},
        };
        public string FileName => file.FileName;
        public Stream Stream => file.OpenReadStream();
        public string ContentType => file.ContentType;
        public string Extension => Path.GetExtension(FileName);
        public FileType Type => GetFileType();
        private string SingleType => ContentType.Split("/")[0];
        private FileType GetFileType()
        {
            if (ContentType.Contains("gif")) return FileType.Gif;

            if (types.TryGetValue(SingleType, out var type)) return type;

            return FileType.Desconocido;
        }
    }
}