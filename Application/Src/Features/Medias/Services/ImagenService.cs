using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Medias.Services;

public class ImagenService : IMediaService
{
    private readonly MiniaturaService _miniaturaService;
    public ImagenService(MiniaturaService miniaturaService)
    {
        _miniaturaService = miniaturaService;
    }

    public async Task<Media> Create(string path)
    {
        string miniatura = await _miniaturaService.Procesar(path);

        return new Media(
            MediaProvider.Imagen,
            "/media/thumbnails/" + Path.GetFileName(miniatura),
            null
        );
    }
}