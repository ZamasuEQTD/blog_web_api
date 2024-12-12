using Application.Features.Medias.Abstractions;
using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Features.Medias.Services;

public class EmbedProcesador
{
    private readonly IHasher _hasher;
    private readonly IMediasRepository _repository;
    private readonly IEmbedMediaFactory _embedFactory;
    public EmbedProcesador(IHasher hasher, IMediasRepository repository, IEmbedMediaFactory embedFactory)
    {
        _hasher = hasher;
        _repository = repository;
        _embedFactory = embedFactory;
    }

    public async Task<HashedMedia> Procesar(IEmbedFile file)
    {
        var hash = await _hasher.Hash(file.Url);
    
        HashedMedia? media = await _repository.GetMediaByHash(hash);

        if (media is not null) return media;

        return new HashedMedia(
            null, 
            hash,
            file.Url,
            await _embedFactory.Create(file.Type).Create(file)
        );
    }
}