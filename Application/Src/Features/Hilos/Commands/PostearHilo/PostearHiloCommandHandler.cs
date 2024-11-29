using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Comentarios.Commands;
using Application.Medias.Abstractions;
using Application.Medias.Services;
using Domain.Categorias;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Media;
using Domain.Media.Abstractions;
using Domain.Media.ValueObjects;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommandHiloCommandHandler : ICommandHandler<PostearHiloCommand, Guid>
    {

        static private readonly List<FileType> ARCHIVOS_SOPORTADOS = [
            FileType.Video,
            FileType.Imagen,
            FileType.Gif,
        ];

        static private readonly List<NetworkSource> NETWORK_SOURCES = [
            NetworkSource.Youtube
        ];
        private readonly MediaProcesador _mediaProcesador;
        private readonly EmbedProcessor _embedProcessor;
        private readonly IHilosRepository _hilosRepository;
        private readonly IMediasRepository _mediasRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;
        public PostearHiloCommandHiloCommandHandler(IUnitOfWork unitOfWork, IUserContext user, IEncuestasRepository encuestasRepository, IMediasRepository mediasRepository, IHilosRepository hilosRepository, MediaProcesador mediaProcesador, EmbedProcessor embedProcessor)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _encuestasRepository = encuestasRepository;
            _mediasRepository = mediasRepository;
            _hilosRepository = hilosRepository;
            _mediaProcesador = mediaProcesador;
            _embedProcessor = embedProcessor;
        }

        public async Task<Result<Guid>> Handle(PostearHiloCommand request, CancellationToken cancellationToken)
        {
            Result<Titulo> titulo = Titulo.Create(request.Titulo);

            if (titulo.IsFailure) return titulo.Error;

            Result<Descripcion> descripcion = Descripcion.Create(request.Descripcion);

            if (descripcion.IsFailure) return descripcion.Error;

            EncuestaId? encuestaId = null;

            if (request.Encuesta.Count != 0)
            {
                var encuesta = Encuesta.Create(request.Encuesta);

                if (encuesta.IsFailure) return encuesta.Error;

                _encuestasRepository.Add(encuesta.Value);

                encuestaId = encuesta.Value.Id;
            }

            MediaReference reference;

            HashedMedia media;

            if (request.File is not null)
            {
                if (!ARCHIVOS_SOPORTADOS.Contains(request.File.Type)) return new Error("Hilos.ArchivoNoSoportado", "Solo se aceptan imagenes y videos para la portada");

                media = await _mediaProcesador.Procesar(request.File);

                request.File.Stream.Dispose();
            }
            else if (request.Embed is not null)
            {
                media = await _embedProcessor.Procesar(request.Embed);
            }
            else return new Error("Hilos.SinPortada");

            reference = new MediaReference(
                media.Id,
                request.Spoiler
            );

            _mediasRepository.Add(reference);

            Hilo hilo = new Hilo(
               titulo.Value,
               descripcion.Value,
               new Autor(_user.Rango == Rango.Moderador ? _user.Username : "Anonimo", _user.Rango.ToRangoDeUsuario()),
               new UsuarioId(_user.UsuarioId),
               reference.Id,
               new SubcategoriaId(request.Subcategoria),
               encuestaId,
               new(
                   request.DadosActivados,
                   request.IdUnicoAtivado
               )
           );

            _hilosRepository.Add(hilo);

            await _unitOfWork.SaveChangesAsync();

            return hilo.Id.Value;
        }
    }

    public interface IMediasRepository
    {
        Task<HashedMedia?> GetMediaByHash(string hash);
        void Add(HashedMedia media);
        void Add(MediaReference reference);
    }
}