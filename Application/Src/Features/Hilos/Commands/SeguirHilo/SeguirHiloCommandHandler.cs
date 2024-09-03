using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class SeguirHiloCommandHandler : ICommandHandler<SeguirHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;

        public SeguirHiloCommandHandler(IUnitOfWork unitOfWork, IUserContext user, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(SeguirHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado;

            RelacionDeHilo? relacion = await _hilosRepository.GetRelacion(hilo.Id, new(_user.UsuarioId));

            if (relacion is null)
            {
                relacion = new(
                    hilo.Id,
                    new UsuarioId(_user.UsuarioId)
                );
            }

            Result result = hilo.Seguir(relacion);

            if (result.IsFailure) return result;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}