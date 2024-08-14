using Domain.Medias.Abstractions;

namespace Domain.Medias.Services {
    public class MiniaturaProcesor {
        private readonly IResizer _resizer;
        private readonly IFileService _fileService;
        private readonly IFolderProvider _folderProvider;
        public MiniaturaProcesor(IResizer resizer, IFileService fileService, IFolderProvider folderProvider)
        {
            _resizer = resizer;
            _fileService = fileService;
            _folderProvider = folderProvider;
        }

        public async Task<string> Procesar(string imagen){
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + Guid.NewGuid() + ".jpeg";
            
            Stream resized = await _resizer.Resize(
                imagen,
                200,
                200
            );

            await _fileService.GuardarArchivo(resized, miniatura_path);

            resized.Dispose();

            return miniatura_path;
        }  
    }
}