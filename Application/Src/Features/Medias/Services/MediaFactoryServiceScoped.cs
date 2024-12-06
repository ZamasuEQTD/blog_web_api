using Application.Medias.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Medias.Services
{
    public class MediaFactoryServiceScoped : IMediaFactory
    {
        private readonly IServiceProvider _serviceProvider;
        public MediaFactoryServiceScoped(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public IMediaService Create(FileType tipo)
        {
            return tipo switch
            {
                FileType.Imagen => _serviceProvider.GetRequiredService<ImagenService>(),
                FileType.Video => _serviceProvider.GetRequiredService<VideoService>(),
                FileType.Gif => _serviceProvider.GetRequiredService<GifService>(),
                _ => throw new ArgumentException($"Tipo de media inválido: {tipo}"),
            };
        }
    }
}