using Application.Common.Services;
using Domain.Abstractions;
using Domain.Medias;
using Domain.Medias.Abstractions;

namespace Application.Medias.Services
{
    public class FileMediaProcesor
    {
        private readonly IFolderProvider _folders;
        private readonly IStrategyContext _strategy;
        private readonly IFileService _fileService;
        private readonly IMediasRepository _mediaRepository;
        public async Task<HashedMediaProvider> Procesar(IFileProvider file)
        {
            Stream stream = file.Stream;

            string hash = await Hasher.Hash(stream);

            HashedMediaProvider? provider = await _mediaRepository.GetHashedMedia(hash);

            if (provider is not null) return provider;

            string path = _folders.FilesFolder + "/" + Guid.NewGuid() + file.Extension;

            await _fileService.GuardarArchivo(stream, path);

            Media media = await _strategy.ExecuteStrategy<MediaType, MediaProcesorParams, Media, IMediaProcesorStrategy>(
                file.Media,
                new MediaProcesorParams()
                {
                    Path = path,
                    ContentType = file.MimeType,
                    Filename = file.FileName,
                    Stream = stream
                },
                default
            );

            _mediaRepository.Add(media);

            provider = new HashedMediaProvider(
                hash,
                media.Id
            );

            _mediaRepository.Add(provider);

            return provider;
        }


    }
    public interface IMediaProvider
    {
        bool Spoiler { get; }
    }

    public interface IFileProvider : IMediaProvider
    {
        string Extension { get; }
        string FileName { get; }
        string MimeType { get; }
        Stream Stream { get; }
        MediaType Media { get; }
    }

    public interface IUrlProvider : IMediaProvider
    {
        string Url { get; }
        UrlTypeProvider Provider { get; }
    }

    public enum UrlTypeProvider
    {
        Youtube
    }
    public enum MediaType
    {
        Imagen,
        Gif,
        Video,
        Desconocido
    }
}