using Application.Medias.Abstractions;

namespace Application.Medias.Services;

public class MiniaturaService
{
    private readonly IImageResizer _resizer;
    private readonly IMediaFolderProvider _folderProvider;
    private readonly IFileService _fileService;
    public MiniaturaService(IImageResizer resizer, IMediaFolderProvider folderProvider, IFileService fileService)
    {
        _resizer = resizer;
        _folderProvider = folderProvider;
        _fileService = fileService;
    }

    public async Task<string> Procesar(string imagen)
        {
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + Path.GetFileNameWithoutExtension(imagen) + ".jpeg";

            using Stream resized = await _resizer.Resize(
                imagen,
                200,
                200
            );

            await _fileService.GuardarArchivo(resized, miniatura_path);

            return miniatura_path;
        }

        public async Task<string> Procesar(Stream stream, string hash)
        {
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + hash + ".jpeg";

            using Stream resized = await _resizer.Resize(
                stream,
                200,
                200
            );

            await _fileService.GuardarArchivo(resized, miniatura_path);

            return miniatura_path;
        }
}