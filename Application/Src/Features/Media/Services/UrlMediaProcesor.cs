using Application.Common.Services;
using Domain.Abstractions;
using Domain.Medias;
using Domain.Medias.Abstractions;
using static Application.Medias.Services.FileMediaProcesor;

namespace Application.Medias.Services
{
    public class UrlMediaProcesor
    {
        private readonly IMediasRepository _mediasRepository;
        private readonly IStrategyContext _strategy;
        public async Task<HashedMediaProvider> Procesar(IUrlProvider provider)
        {
            string hash = await Hasher.Hash(provider.Url);

            HashedMediaProvider? hashed = await _mediasRepository.GetHashedMedia(hash);

            if (hashed is not null) return hashed;

            Media media = await _strategy.ExecuteStrategy<UrlTypeProvider, string, Media, IUrlMediaProcesorStrategy>(provider.Provider, provider.Url, default);

            hashed = new HashedMediaProvider(
                hash,
                media.Id
            );

            _mediasRepository.Add(media);

            _mediasRepository.Add(hashed);

            return hashed;
        }
    }
}