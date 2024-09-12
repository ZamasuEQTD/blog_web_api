using Application.Hilos.Commands;
using Application.Medias.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;

namespace Application.Medias.Services
{
    public class MediaProcesador
    {
        private readonly IHasherCalculator _hasher;
        private readonly IFileService _fileService;
        private readonly IMediasRepository _repository;
        private readonly IFolderProvider _folderProvider;
        private readonly IFileContextStrategy _proccessor;

        public MediaProcesador(IFileContextStrategy proccessor, IFolderProvider folderProvider, IMediasRepository repository, IFileService fileService, IHasherCalculator hasher)
        {
            _proccessor = proccessor;
            _folderProvider = folderProvider;
            _repository = repository;
            _fileService = fileService;
            _hasher = hasher;
        }

        public async Task<HashedMedia> Procesar(IFile file)
        {
            Stream stream = file.Stream;

            string hash = await _hasher.Hash(stream);

            HashedMedia? media = await _repository.GetMediaByHash(hash);

            if (media is not null) return media;

            string media_path = _folderProvider.FilesFolder + "/" + Guid.NewGuid() + file.Extension;

            await _fileService.GuardarArchivo(stream, media_path);

            HashedMedia m = await _proccessor.Execute(
                file.Type,
                new FileProcesorParams(
                    media_path,
                    file.FileName,
                    hash
                )
            );

            _repository.Add(m);

            return m;
        }
    }
}