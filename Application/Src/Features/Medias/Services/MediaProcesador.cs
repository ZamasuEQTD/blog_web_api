using Application.Abstractions;
using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Features.Medias.Models;

namespace Application.Medias.Services;

public class MediaProcesador
{
    private readonly IHasher _hasher;
    private readonly IFileService _fileService;
    private readonly IMediasRepository _repository;
    private readonly IMediaFolderProvider _folderProvider;
    private readonly IMediaFactory _mediaFactory;

    private readonly IUrlProvider _url;

    public MediaProcesador(IHasher hasher, IFileService fileService, IMediasRepository repository, IMediaFolderProvider folderProvider, IMediaFactory mediaFactory, IUrlProvider url)
    {
        _hasher = hasher;
        _fileService = fileService;
        _repository = repository;
        _folderProvider = folderProvider;
        _mediaFactory = mediaFactory;
        _url = url;
    }

    public async Task<HashedMedia> Procesar(IFile file)
    {
        Stream stream = file.Stream;

        string hash = await _hasher.Hash(stream);

        HashedMedia? media = await _repository.GetMediaByHash(hash);

        if (media is not null) return media;

        string media_path = _folderProvider.FilesFolder + "/" + Guid.NewGuid() + file.Extension;

        await _fileService.GuardarArchivo(stream, media_path);

        return new HashedMedia(
            file.FileName,
            hash,
            _url.Files + Path.GetFileName(media_path),
            await _mediaFactory.Create(file.Type).Create(media_path)
        );
    }
}
