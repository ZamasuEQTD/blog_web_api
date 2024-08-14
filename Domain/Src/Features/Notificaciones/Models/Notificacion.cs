using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Notificaciones
{
    public abstract class Notificacion : Entity<NotificacionId>
    {
        public UsuarioId NotificadoId { get; private set; }
        public NotificacionStatus Status { get; private set; }

        protected Notificacion(UsuarioId usuarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.NotificadoId = usuarioId;
            this.Status = NotificacionStatus.SinLeer;
        }

        public Result Leer(UsuarioId usuarioId)
        {
            if (usuarioId != NotificadoId) return NotificacionesFailures.NoTePertenece;

            this.Status = NotificacionStatus.Leida;

            return Result.Success();
        }

        public enum NotificacionStatus
        {
            Leida,
            SinLeer
        }
    }
    public class NotificacionId : EntityId
    {
        public NotificacionId(Guid id) : base(id) { }
    }

    static public class NotificacionesFailures
    {
        public readonly static Error NoTePertenece = new Error("");
    }
}