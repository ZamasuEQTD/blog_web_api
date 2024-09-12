using Domain.Media;
using Domain.Media.ValueObjects;

namespace Application.Medias.Abstractions
{
    public interface IEmbedContextProcesorStrategy
    {
        Task<NetworkMedia> Procesar(NetworkSource source, EmbedStrategyParams @params);
    }
}