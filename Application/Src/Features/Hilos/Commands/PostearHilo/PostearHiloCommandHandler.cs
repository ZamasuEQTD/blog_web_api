using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Medias.Services;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Medias;
using Domain.Medias.Abstractions;
using SharedKernel;
using static Application.Medias.Services.FileMediaProcesor;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommandHiloCommandHandler : ICommandHandler<PostearHiloCommand, Guid>
    {
        private static HashSet<MediaType> _medias = [MediaType.Gif, MediaType.Imagen, MediaType.Gif];
        private readonly FileMediaProcesor _fileProcesor;
        private readonly UrlMediaProcesor _urlMediaProcesor;
        private readonly IHilosRepository _hilosRepository;
        private readonly IMediasRepository _mediasRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Result<Guid>> Handle(PostearHiloCommand request, CancellationToken cancellationToken)
        {
            Result<Titulo> titulo = Titulo.Create(request.Titulo);
            Result<Descripcion> descripcion = Descripcion.Create(request.Descripcion);

            HashedMediaProvider hashed;

            if (request.PortadaFile is not null && _medias.Contains(request.PortadaFile.Media))
            {
                hashed = await _fileProcesor.Procesar(request.PortadaFile);
            }
            else if (request.PortadaUrl is not null)
            {
                hashed = await _urlMediaProcesor.Procesar(request.PortadaUrl);
            }
            else
            {
                return HilosFailures.SinPortada;
            }

            MediaReference portada = new MediaReference(hashed.Id, request.Spoiler);

            _mediasRepository.Add(portada);

            Hilo hilo = new Hilo(
                titulo.Value,
                descripcion.Value,
                portada.Id,
                new(request.Subcategoria),
                new(_user.UsuarioId),
                null,
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
}