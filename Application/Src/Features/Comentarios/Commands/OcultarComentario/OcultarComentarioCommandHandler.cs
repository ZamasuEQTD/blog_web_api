using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Hilos;
using Domain.Hilos.Abstractions;

namespace Application.Comentarios.Commands
{
    public class OcultarComentarioCommandHandler : ICommandHandler<OcultarComentarioCommand>{
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;
        public async Task Handle(OcultarComentarioCommand request, CancellationToken cancellationToken) 
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new (request.Hilo));
        
            if(hilo is null) throw new HiloNoEncontrado();

            hilo.OcultarComentario(new (request.Comentario), new (_context.UsuarioId));

            await _unitOfWork.SaveChangesAsync();
        }
    }
}