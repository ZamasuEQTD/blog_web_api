using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Stickies;
using MediatR;
using SharedKernel;
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

        public async Task<Result> Handle(EliminarStickyCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado; ;

            var result = hilo.EliminarSticky();

            if (result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

}