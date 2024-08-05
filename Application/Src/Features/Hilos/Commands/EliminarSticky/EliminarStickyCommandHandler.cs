using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel.Abstractions;

namespace Application.Hilos.Commands
{
    public class EliminarStickyCommandHandler : ICommandHandler<EliminarStickyCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;


        public EliminarStickyCommandHandler(IUnitOfWork unitOfWork, IDateTimeProvider timeProvider, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _timeProvider = timeProvider;
            _hilosRepository = hilosRepository;
        }

        public async Task Handle(EliminarStickyCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.EliminarSticky(_timeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}