using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Services;

namespace Application.Medias.Services
{
    public class YoutubeEmbedProcesor : IEmbedProcesorStrategy
    {

        public async Task<NetworkMedia> Procesar(EmbedStrategyParams @params)
        {
            return new YoutubeVideo(
                 @params.Hash,
                 @params.Url,
                 YoutubeService.GetVideoThumbnailFromUrl(@params.Url),
                 YoutubeService.GetVideoThumbnailFromUrl(@params.Url)
             );
        }
    }
}