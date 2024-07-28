using Domain.Media.Abstractions;

namespace Infraestructure.Media
{
    public class FilesService : IFileService {
        public async Task GuardarArchivo(Stream stream, string path)
        {
            stream.Seek(0, SeekOrigin.Begin);

            using (FileStream fileStream = new(path, FileMode.Create, FileAccess.Write))
            {
                // Copia el contenido del IFormFile al stream
                await stream.CopyToAsync(fileStream);
                await fileStream.DisposeAsync();
            }
        }
    }
}