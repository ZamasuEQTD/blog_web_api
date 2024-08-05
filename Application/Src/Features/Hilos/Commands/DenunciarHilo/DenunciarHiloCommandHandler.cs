using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;

namespace Application.Hilos.Commands
{
    public class DenunciarHiloCommandHandler : ICommandHandler<DenunciarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;
        public async Task Handle(DenunciarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new (request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.Denunciar(
                new UsuarioId(_user.UsuarioId)
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}