using Application.Medias.Abstractions;

namespace Infraestructure.Media
{
    public class FileService : IFileService
    {
        public async Task GuardarArchivo(Stream stream, string path)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using (FileStream fileStream = new(path, FileMode.Create, FileAccess.Write))
            {
                await stream.CopyToAsync(fileStream);
                await fileStream.DisposeAsync();
            }
        }
    }
}