using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Hilos.Commands
{
    public class EliminarHiloCommandHandler : ICommandHandler<EliminarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IDateTimeProvider _timeProvider;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EliminarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) throw new HiloNoEncontrado();

            var result = await hilo.Eliminar(
                _hilosRepository,
                _timeProvider.UtcNow
            );

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}