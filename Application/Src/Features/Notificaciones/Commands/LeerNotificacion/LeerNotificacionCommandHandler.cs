using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;

namespace Application.Notificaciones.Commands
{
    public class LeerNotificacionCommandHandler : ICommandHandler<LeerNotificacionCommand> {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public LeerNotificacionCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IUsuariosRepository usuariosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _usuariosRepository = usuariosRepository;
        }

        public async Task Handle(LeerNotificacionCommand request, CancellationToken cancellationToken) {
            Usuario usuario = (await _usuariosRepository.GetUsuarioById(new(_context.UsuarioId)))!;
            
            usuario.LeerNotificacion(new(request.Notificacion));
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}