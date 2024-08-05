using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;

namespace Application.Comentarios.Commands
{
    public class DenunciarComentarioCommandHandler : ICommandHandler<DenunciarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DenunciarComentarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _hilosRepository = hilosRepository;
        }

        public async Task Handle(DenunciarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.Denunciar(
                new ComentarioId(request.Comentario),
                new UsuarioId(_context.UsuarioId) 
            );

            await _unitOfWork.SaveChangesAsync();
        }
    }
}