using Application.Features.Medias.Abstractions;
using Application.Medias.Abstractions;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Features.Medias.Services;

public class EmbedMediaFactoryServiceScoped : IEmbedMediaFactory
{
    private readonly IServiceProvider _serviceProvider;
    public EmbedMediaFactoryServiceScoped(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public IEmbedService Create(EmbedType file)
    {
        return file switch
        {
            EmbedType.Youtube => _serviceProvider.GetRequiredService<YoutubeEmbedService>(),
            _ => throw new NotImplementedException()
        };
    }
}