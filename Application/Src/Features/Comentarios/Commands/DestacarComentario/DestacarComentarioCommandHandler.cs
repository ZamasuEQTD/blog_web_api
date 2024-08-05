using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Hilos;
using Domain.Hilos.Abstractions;

namespace Application.Comentarios.Commands
{
    public class DestacarComentarioCommandHandler : ICommandHandler<DestacarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user; 
        private readonly IUnitOfWork _unitOfWork;
        public async Task Handle(DestacarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.DestacarComentario(
                new (request.Hilo),
                new (_user.UsuarioId)
            );

            await _unitOfWork.SaveChangesAsync();
        }
    }
}