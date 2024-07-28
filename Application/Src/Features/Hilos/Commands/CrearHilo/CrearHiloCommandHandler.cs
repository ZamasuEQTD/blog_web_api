using System.Drawing;
using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Encuestas.Services;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Media.Abstractions.Strategies;
using Domain.Media.Services;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class CrearHiloCommandHandler : ICommandHandler<CrearHiloCommand, Guid>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _userContext;
        private readonly IMediasRepository _mediasRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly EncuestaOrchetastor _encuestaOrchetastor;
        private readonly MediaProcesor _mediaProcesor;
        public CrearHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, IUserContext userContext, IMediasRepository mediasRepository, IEncuestasRepository encuestasRepository, EncuestaOrchetastor encuestaOrchetastor, MediaProcesor mediaProcesor)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _userContext = userContext;
            _mediasRepository = mediasRepository;
            _encuestasRepository = encuestasRepository;
            _encuestaOrchetastor = encuestaOrchetastor;
            _mediaProcesor = mediaProcesor;
        }

        public async Task<Result<Guid>> Handle(CrearHiloCommand request, CancellationToken cancellationToken) {
            MediaReferenceId portadaId = new MediaReferenceId(Guid.NewGuid());
            
            EncuestaId? encuestaId = request.Encuesta.Any() ? new(Guid.NewGuid()) : null;

            Hilo hilo = Hilo.Create(
                new(Guid.NewGuid()),
                new(_userContext.UsuarioId),
                portadaId,
                new ConfiguracionDeComentarios(
                    request.Configuracion.Dados,
                    request.Configuracion.TagUnico
                ),
                encuestaId,
                request.Titulo,
                request.Descripcion
            );

            if(encuestaId is not null){
                var result = _encuestaOrchetastor.Orquestar(encuestaId,request.Encuesta!);

                if(result.IsFailure) return result.Error;

                _encuestasRepository.Add(result.Value);
            }

            MediaSource media = await _mediaProcesor.Procesar(request.Portada);

            _mediasRepository.Add(new MediaReference(portadaId, media.Id, request.EsSpoiler));

            _hilosRepository.Add(hilo);

            await _unitOfWork.SaveChangesAsync();

            return hilo.Id.Value;
        }
    }


    public class MediaProcesor  {
        private readonly IMediaHasher _hasher;
        private readonly IFileService _fileService;
        private readonly IMediasRepository _repository;
        private readonly IFolderProvider _folderProvider;
        private readonly IMediaProcesorsStrategy _mediaProcesor;

        public MediaProcesor(IFolderProvider folderProvider, IMediasRepository repository, IMediaHasher hasher, IMediaProcesorsStrategy mediaProcesor, IFileService fileService)
        {
            _folderProvider = folderProvider;
            _repository = repository;
            _hasher = hasher;
            _mediaProcesor = mediaProcesor;
            _fileService = fileService;
        }

        public async Task<MediaSource> Procesar(IFileProvider file){

            Stream stream = file.Stream;
            string hash = await _hasher.HashStream(stream);

            HashedMedia? media = await _repository.GetHashedMediaByHash(hash);

            if(media is not null) return media;

            string media_path = _folderProvider.FilesFolder+  "/" + Guid.NewGuid() + file.Extension;
            
            await _fileService.GuardarArchivo(stream, media_path);
            
            stream.Dispose();

            media = new HashedMedia(
                new MediaSourceId(Guid.NewGuid()),
                await _mediaProcesor.Procesar(media_path),
                hash
            );

            _repository.Add(media);

            return media;
        }
    }

    public interface IFileProvider {
        string Extension { get; }
        string FileName { get; }
        string MimeType { get; }
        Stream Stream {get;}
        MediaType Media { get; }	
    }
    
    public enum MediaType {
        Imagen,
        Gif,
        Video,
        Desconocido
    }

    
}