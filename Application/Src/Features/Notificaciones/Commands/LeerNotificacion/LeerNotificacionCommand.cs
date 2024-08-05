using Application.Abstractions.Messaging;

namespace Application.Notificaciones.Commands
{
    public class LeerNotificacionCommand : ICommand {
        public Guid Notificacion { get; set; }
    }
}