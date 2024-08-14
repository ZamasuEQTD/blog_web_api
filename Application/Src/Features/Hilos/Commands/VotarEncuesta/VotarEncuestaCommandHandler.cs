using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class VotarEncuestaCommandHandler : ICommandHandler<VotarEncuestaCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;

        public VotarEncuestaCommandHandler(IUnitOfWork unitOfWork, IEncuestasRepository encuestasRepository, IHilosRepository hilosRepository, IUserContext user)
        {
            _unitOfWork = unitOfWork;
            _encuestasRepository = encuestasRepository;
            _hilosRepository = hilosRepository;
            _user = user;
        }

        public async Task<Result> Handle(VotarEncuestaCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado; ;

            var result = await hilo.Votar(
                _encuestasRepository,
                new(_user.UsuarioId),
                new(request.Respuesta)
            );

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}