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
    public class EliminarComentarioCommandHandler : ICommandHandler<EliminarComentarioCommand>
    {

        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;

        public async Task Handle(EliminarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if(hilo is null) throw new HiloNoEncontrado();

            hilo.Eliminar(
                new ComentarioId(request.Comentario),
                new UsuarioId(_user.UsuarioId)
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}