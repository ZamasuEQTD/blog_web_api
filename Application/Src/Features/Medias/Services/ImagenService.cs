using Application.Abstractions;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Medias.Services;

public class ImagenService : IMediaService
{
    private readonly MiniaturaService _miniaturaService;
    private readonly IUrlProvider _url;
    public ImagenService(MiniaturaService miniaturaService, IUrlProvider url)
    {
        _miniaturaService = miniaturaService;
        _url = url;
    }

    public async Task<Media> Create(string path)
    {
        string miniatura = await _miniaturaService.Procesar(path);

        return new Media(
            MediaProvider.Imagen,
            _url.Thumbnail + Path.GetFileName(miniatura),
            null
        );
    }
}