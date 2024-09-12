using Domain.Media;

namespace Application.Medias.Abstractions
{

    public record EmbedStrategyParams(string Url, string Hash);
    public interface IEmbedProcesorStrategy
    {
        Task<NetworkMedia> Procesar(EmbedStrategyParams @params);
    }
}