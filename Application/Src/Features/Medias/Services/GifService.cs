using Application.Abstractions;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;
using static Application.Medias.Services.VideoService;

namespace Application.Medias.Services;

public class GifService : IMediaService
{
    private readonly GifVideoPrevisualizadorProcesador _gifVideoPrevisualizadorProcesador;
    private readonly MiniaturaService _miniaturaService;
    private readonly IUrlProvider _url;
    public GifService(GifVideoPrevisualizadorProcesador gifVideoPrevisualizadorProcesador, MiniaturaService miniaturaService, IUrlProvider url)
    {
        _gifVideoPrevisualizadorProcesador = gifVideoPrevisualizadorProcesador;
        _miniaturaService = miniaturaService;
        _url = url;
    }

    public async Task<Media> Create(string path)
    {
        string previsualizacion = await _gifVideoPrevisualizadorProcesador.Procesar(path);

        string miniatura = await _miniaturaService.Procesar(previsualizacion);

        return new Media(
            MediaProvider.Gif,
            _url.Thumbnail + Path.GetFileName(miniatura),
            _url.Previsualizacion + Path.GetFileName(previsualizacion)
        );
    }
}