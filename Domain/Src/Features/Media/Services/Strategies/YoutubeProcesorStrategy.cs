using Domain.Medias.Abstractions;

namespace Domain.Medias.Services
{
    public class YoutubeProcesorStrategy : IUrlMediaProcesorStrategy
    {
        public async Task<Media> Execute(string request, CancellationToken cancellationToken)
        {
            string miniatura = YoutubeUtils.GetVideoThumbnailFromUrl(request);

            return await Task.FromResult(new YoutubeVideo(
                request,
                miniatura,
                miniatura
            ));
        }
    }
}