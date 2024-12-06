using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;
using static Application.Medias.Services.VideoService;

namespace Application.Medias.Services;

public class GifMediaService : IMediaService
{
    private readonly GifVideoPrevisualizadorProcesador _gifVideoPrevisualizadorProcesador;
    private readonly MiniaturaService _miniaturaService;
    public GifMediaService(GifVideoPrevisualizadorProcesador gifVideoPrevisualizadorProcesador, MiniaturaService miniaturaService)
    {
        _gifVideoPrevisualizadorProcesador = gifVideoPrevisualizadorProcesador;
        _miniaturaService = miniaturaService;
    }

    public async Task<Media> Create(string path)
    {
        string previsualizacion = await _gifVideoPrevisualizadorProcesador.Procesar(path);

        string miniatura = await _miniaturaService.Procesar(previsualizacion);

        return new Media(
            MediaProvider.Gif,
            miniatura,
            previsualizacion
        );
    }
}