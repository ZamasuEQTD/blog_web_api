using Application.Abstractions;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Medias.Services;

public class VideoService : IMediaService
{
    private readonly MiniaturaService _miniaturaService;
    private readonly GifVideoPrevisualizadorProcesador _gifVideoPrevisualizadorProcesador;
    private readonly IUrlProvider _url;
    public VideoService(MiniaturaService miniaturaService, GifVideoPrevisualizadorProcesador gifVideoPrevisualizadorProcesador, IUrlProvider url)
    {
        _miniaturaService = miniaturaService;
        _gifVideoPrevisualizadorProcesador = gifVideoPrevisualizadorProcesador;
        _url = url;
    }


    public async Task<Media> Create(string path)
    {

        string previsualizacion = await _gifVideoPrevisualizadorProcesador.Procesar(path);

        string miniatura = await _miniaturaService.Procesar(previsualizacion);

        return new Media(
            MediaProvider.Video,
            _url.Thumbnail + Path.GetFileName(miniatura),
            _url.Previsualizacion + Path.GetFileName(previsualizacion)
        );
    }


    public class GifVideoPrevisualizadorProcesador
    {
        private readonly IVideoGifPrevisualizadorService _videoGifPrevisualizadorService;
        private readonly IMediaFolderProvider _folderProvider;
        private readonly IFileService _fileService;
        public GifVideoPrevisualizadorProcesador(IMediaFolderProvider folderProvider, IFileService fileService, IVideoGifPrevisualizadorService videoGifPrevisualizadorService)
        {
            _folderProvider = folderProvider;
            _fileService = fileService;
            _videoGifPrevisualizadorService = videoGifPrevisualizadorService;
        }

        public async Task<string>Procesar(string path)
        {
            string previsualizacion_path = _folderProvider.Previsualizaciones + "/" + Path.GetFileNameWithoutExtension(path) + ".png";

            using Stream vista_previa = _videoGifPrevisualizadorService.Generar(path);

            await _fileService.GuardarArchivo(vista_previa, previsualizacion_path);

            return previsualizacion_path;
        }
    }
}