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

            Result result = hilo.Seguir(new UsuarioId(_user.UsuarioId));

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}