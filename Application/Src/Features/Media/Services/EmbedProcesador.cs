using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;

namespace Application.Medias.Services
{
    public class EmbedProcessor
    {
        private readonly IHasherCalculator _hasher;
        private readonly IEmbedContextProcesorStrategy _procesor;
        private readonly IMediasRepository _repository;
        public EmbedProcessor(IHasherCalculator hasher, IMediasRepository repository, IEmbedContextProcesorStrategy procesor)
        {
            _hasher = hasher;
            _repository = repository;
            _procesor = procesor;
        }

        public async Task<HashedMedia> Procesar(IEmbedFile embed)
        {
            string hash = await _hasher.Hash(embed.Url);

            HashedMedia? media = await _repository.GetMediaByHash(hash);

            if (media is not null) return media;

            NetworkMedia m = await _procesor.Procesar(embed.Source, new(embed.Url, hash));

            _repository.Add(m);

            return m;
        }
    }
}