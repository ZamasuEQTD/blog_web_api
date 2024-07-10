using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class VotarEncuestaCommandHandler : ICommandHandler<VotarEncuestaCommand> {
        private readonly IHilosRepository _hilosRepository;
        private readonly IEncuestasRepository _encuestasRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserContext _userContext;
        public VotarEncuestaCommandHandler(IUnitOfWork unitOfWork, IEncuestasRepository encuestasRepository, IHilosRepository hilosRepository, IUserContext userContext)
        {
            _unitOfWork = unitOfWork;
            _encuestasRepository = encuestasRepository;
            _hilosRepository = hilosRepository;
            _userContext = userContext;
        }

        public async Task<Result> Handle(VotarEncuestaCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new Hilo.HiloId(request.HiloId));
            
            if(hilo is null){
                return HilosFailures.HILO_INEXISTENTE;
            }

            Encuesta? encuesta = await _encuestasRepository.GetEncuestaById(hilo.EncuestaId!);

            var result = hilo.VotarEncuesta(encuesta!,new RespuestaId(request.RespuestaId), new (_userContext.UsuarioId));

            if(result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}