using System.Drawing;
using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Media;
using Domain.Media.Abstractions;
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
        public CrearHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, IUserContext userContext, IMediasRepository mediasRepository, IEncuestasRepository encuestasRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _userContext = userContext;
            _mediasRepository = mediasRepository;
            _encuestasRepository = encuestasRepository;
        }

        public async Task<Result<Guid>> Handle(CrearHiloCommand request, CancellationToken cancellationToken) {
            MediaReferenceId portadaId = new MediaReferenceId(Guid.NewGuid());
            EncuestaId? encuestaId = request.Encuesta is not null? new(Guid.NewGuid()) : null;

            Hilo hilo = Hilo.Create(
                new(Guid.NewGuid()),
                new(Guid.NewGuid()),
                portadaId,
                encuestaId,
                request.Titulo,
                request.Descripcion
            );

            if(encuestaId is not null){
            }

            Media media = new Media(
                new(Guid.NewGuid()),
                "good"                                                                                                                                              
            );
            
            _hilosRepository.Add(hilo);

            _mediasRepository.Add(new MediaReference(portadaId, media.Id, request.EsSpoiler));

            await _unitOfWork.SaveChangesAsync();

            return hilo.Id.Value;
        }
    }
}