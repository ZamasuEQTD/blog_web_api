using Application.Features.Medias.Abstractions;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;
using Domain.Features.Medias.Services;

namespace Application.Features.Medias.Services;

public class YoutubeEmbedService : IEmbedService
{
    public Task<Media> Create(IEmbedFile url)
    {
        return Task.FromResult(new Media(
            MediaProvider.Youtube,
            YoutubeService.GetVideoThumbnailFromUrl(url.Url),
            YoutubeService.GetVideoThumbnailFromUrl(url.Url)
        ));
    }
}