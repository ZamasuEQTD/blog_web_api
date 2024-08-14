using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class DenunciarHiloCommandHandler : ICommandHandler<DenunciarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Result> Handle(DenunciarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) throw new HiloNoEncontrado();

            Result<DenunciaDeHilo> result = await hilo.Denunciar(
                _hilosRepository,
                new UsuarioId(_user.UsuarioId)
            );

            if (result.IsFailure) return result.Error;

            _hilosRepository.Add(result.Value);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}