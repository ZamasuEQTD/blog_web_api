using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.ValueObjects;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Medias.Services
{
    public class EmbedContextStrategy(IServiceProvider serviceProvider) : IEmbedContextProcesorStrategy
    {
        public async Task<NetworkMedia> Procesar(NetworkSource source, EmbedStrategyParams @params)
        {
            var strategy = serviceProvider.GetKeyedService<IEmbedProcesorStrategy>(source);

            if (strategy is null) throw new ArgumentException("Strategy no conseguida");

            return await strategy!.Procesar(@params);
        }
    }
}