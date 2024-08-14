using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Notificaciones;
using Domain.Notificaciones.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;

namespace Application.Notificaciones.Commands
{
    public class LeerNotificacionesCommandHandler : ICommandHandler<LeerNotificacionesCommand>
    {

        private readonly IUserContext _context;
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly IUnitOfWork _unitOfWork;

        public LeerNotificacionesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(LeerNotificacionesCommand request, CancellationToken cancellationToken)
        {
            List<Notificacion> notificaciones = await _notificacionesRepository.GetNotificaciones(new UsuarioId(_context.UsuarioId));

            foreach (var n in notificaciones)
            {
                n.Leer(
                    new(_context.UsuarioId)
                );
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}