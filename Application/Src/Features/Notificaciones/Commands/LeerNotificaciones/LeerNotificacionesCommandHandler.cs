using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;

namespace Application.Notificaciones.Commands
{
    public class LeerNotificacionesCommandHandler : ICommandHandler<LeerNotificacionesCommand> {

        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LeerNotificacionesCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(LeerNotificacionesCommand request, CancellationToken cancellationToken) {
            
            Usuario usuario = ( await _usuariosRepository.GetUsuarioById(new(_context.UsuarioId)))!;

            usuario.LeerTodasLasNotificaciones();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}