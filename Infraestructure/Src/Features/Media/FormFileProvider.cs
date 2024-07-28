using Application.Hilos.Commands;
using Microsoft.AspNetCore.Http;

namespace Infraestructure.Media
{
    public class FormFileProvider(IFormFile file) : IFileProvider {
        public string Extension => Path.GetExtension(FileName);
        public string FileName => file.FileName;
        public Stream Stream => file.OpenReadStream();
        public MediaType Media => ExtensionFileService.GetTypeFromMime(MimeType);
        public string MimeType => "video";
    }

    public static class ExtensionFileService
    {
        public static MediaType GetTypeFromMime(string mime){
            if(mime.Contains("video")) return MediaType.Video;
            if(mime.Contains("gif")) return MediaType.Gif;
            if(mime.Contains("imagen")) return MediaType.Imagen;
            throw new ArgumentException(mime + " Invalid mime");
        }
    }
}