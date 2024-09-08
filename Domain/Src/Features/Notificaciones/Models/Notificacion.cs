using Domain.Comentarios;
using Domain.Hilos;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Notificaciones
{
    public abstract class Notificacion : Entity<NotificacionId>
    {
        public UsuarioId NotificadoId { get; private set; }
        public NotificacionStatus Status { get; private set; }

        protected Notificacion() { }
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

        public bool EsUsuarioNotificado(UsuarioId usuarioId) => this.NotificadoId == usuarioId;
    }
    public class NotificacionId : EntityId
    {
        public NotificacionId(Guid id) : base(id) { }
    }

    static public class NotificacionesFailures
    {
        public readonly static Error NoTePertenece = new Error("");
    }

    public class HiloInteraccionNotificacion : Notificacion
    {
        public HiloId Hilo { get; private set; }
        public ComentarioId ComentarioId { get; private set; }
        public HiloInteraccionNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId) : base(usuarioId)
        {
            Hilo = hilo;
            ComentarioId = comentarioId;
        }
    }
}