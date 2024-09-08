using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Notificaciones;
using Domain.Notificaciones.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Notificaciones.Commands
{
    public class LeerNotificacionCommandHandler : ICommandHandler<LeerNotificacionCommand>
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;


        public async Task<Result> Handle(LeerNotificacionCommand request, CancellationToken cancellationToken)
        {
            Notificacion notificacion = await _notificacionesRepository.GetNotificacion(new NotificacionId(request.Notificacion));

            UsuarioId usuarioId = new UsuarioId(_context.UsuarioId);

            if (!notificacion.EsUsuarioNotificado(usuarioId)) return NotificacionesFailures.NoTePertenece;

            Result result = notificacion.Leer(
                usuarioId
            );

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }


}