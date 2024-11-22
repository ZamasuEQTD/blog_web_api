using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Stickies;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class EstablecerStickyCommandHandler : ICommandHandler<EstablecerStickyCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EstablecerStickyCommandHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EstablecerStickyCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado;

            Result result = hilo.EstablecerSticky();

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}