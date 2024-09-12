using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;

namespace Application.Medias.Services
{
    public class EmbedProcessor
    {
        private readonly IHasherCalculator _hasher;
        private readonly IEmbedProcesorStrategy _procesor;
        private readonly IMediasRepository _repository;
        public EmbedProcessor(IHasherCalculator hasher, IEmbedProcesorStrategy procesor, IMediasRepository repository)
        {
            _hasher = hasher;
            _procesor = procesor;
            _repository = repository;
        }

        public async Task<HashedMedia> Procesar(IEmbedFile embed)
        {
            string hash = await _hasher.Hash(embed.Url);

            HashedMedia? media = await _repository.GetMediaByHash(hash);

            if (media is not null) return media;

            return await _procesor.Procesar(new(embed.Url, hash));
        }
    }
}