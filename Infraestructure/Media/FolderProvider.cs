using Application.Medias.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace Infraestructure.Media
{
    public class FolderProvider(IWebHostEnvironment environment) : IFolderProvider
    {
        public string ThumbnailFolder => Path.Join(BaseMediaFolder, "Thumbnails");
        public string FilesFolder => Path.Join(BaseMediaFolder, "Files");
        public string VistasPrevias => Path.Join(BaseMediaFolder, "VistasPrevias");
        private string BaseMediaFolder => Path.Join(environment.ContentRootPath, "Media");
    }
}