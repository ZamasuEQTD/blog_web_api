using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Hilos;
using Domain.Hilos.Abstractions;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommandHandler : ICommandHandler<ComentarHiloCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;
        public async Task Handle(ComentarHiloCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new (request.Hilo));

            if(hilo is null ) throw new HiloNoEncontrado();


            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}