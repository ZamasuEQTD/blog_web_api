using System.Net.NetworkInformation;
using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;

namespace Application.Hilos.Commands {
    public class EliminarHiloCommandHandler : ICommandHandler<EliminarHiloCommand> {

        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarHiloCommandHandler(
            IUnitOfWork unitOfWork, 
            IHilosRepository hilosRepository
        ){
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EliminarHiloCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.HiloId));

            if (hilo is  null) return HilosFailures.HILO_INEXISTENTE;

            var result = hilo.Eliminar();

            if(result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();
            return Result.Success();
        }
    }
}