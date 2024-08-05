using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel.Abstractions;

namespace Application.Hilos.Commands
{
    public class EliminarHiloCommandHandler : ICommandHandler<EliminarHiloCommand> {
        private readonly IHilosRepository _hilosRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public async Task Handle(EliminarHiloCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new  (request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.Eliminar(_timeProvider.UtcNow);

            await _unitOfWork.SaveChangesAsync();
        }
    }
}