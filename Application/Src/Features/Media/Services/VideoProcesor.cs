using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;

namespace Application.Medias.Services
{
    public class VideoProcesor : IFileProcesorsStrategy
    {
        private readonly MiniaturaProcesor miniaturaProcesor;
        private readonly GifVideoPrevisualizadorProcesador GifVideoPrevisualizadorProcesador;

        public VideoProcesor(MiniaturaProcesor miniaturaProcesor, GifVideoPrevisualizadorProcesador GifVideoPrevisualizadorProcesador)
        {
            this.miniaturaProcesor = miniaturaProcesor;
            this.GifVideoPrevisualizadorProcesador = GifVideoPrevisualizadorProcesador;
        }

        public async Task<FileMedia> Procesar(FileProcesorParams @params)
        {
            var previsulizacion = await GifVideoPrevisualizadorProcesador.Procesar(@params.Media, @params.Hash);

            await miniaturaProcesor.Procesar(previsulizacion, @params.Hash);

            return new Video(
                @params.Hash,
                @params.Media,
                @params.File
            );
        }
    }

    public class GifVideoPrevisualizadorProcesador
    {
        private readonly IVideoPrevisualizadorGenerador _videoPrevisualizadorGenerador;
        private readonly IFolderProvider _folderProvider;
        private readonly IFileService _fileService;
        public GifVideoPrevisualizadorProcesador(IFolderProvider folderProvider, IFileService fileService, IVideoPrevisualizadorGenerador videoPrevisualizadorGenerador)
        {
            _folderProvider = folderProvider;
            _fileService = fileService;
            _videoPrevisualizadorGenerador = videoPrevisualizadorGenerador;
        }

        public async Task<string> Procesar(string path, string hash)
        {
            string previsualizacion_path = _folderProvider.Previsualizaciones + "/" + hash + ".png";

            using Stream vista_previa = _videoPrevisualizadorGenerador.GenerarDesdeVideo(path);

            await _fileService.GuardarArchivo(vista_previa, previsualizacion_path);

            return previsualizacion_path;
        }
        public Stream GenerarStream(string path) => _videoPrevisualizadorGenerador.GenerarDesdeVideo(path);
    }

    public class MiniaturaProcesor
    {
        private readonly IResizer _resizer;
        private readonly IFileService _fileService;
        private readonly IFolderProvider _folderProvider;
        public MiniaturaProcesor(IResizer resizer, IFileService fileService, IFolderProvider folderProvider)
        {
            _resizer = resizer;
            _fileService = fileService;
            _folderProvider = folderProvider;
        }

        public async Task<string> Procesar(string imagen, string hash)
        {
            string miniatura_path = _folderProvider.ThumbnailFolder + "/" + hash + ".jpeg";

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
}