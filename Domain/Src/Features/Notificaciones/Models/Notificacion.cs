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

        public bool EsUsuarioNotificado(UsuarioId usuarioId) => this.NotificadoId == usuarioId;
    }   

    public class NotificacionStatus : ValueObject
    {
        public string Value { get; private set; }
        public NotificacionStatus(string status)
        {
            this.Value = status;
        }

        protected override IEnumerable<object> GetAtomicValues()
        {
            yield return Value;
        }

        public static readonly NotificacionStatus Leida = new("Leida");
        public static readonly NotificacionStatus SinLeer = new("SinLeer");
    }

    public class NotificacionId : EntityId
    {
        public NotificacionId(Guid id) : base(id) { }
    }

    static public class NotificacionesFailures
    {
        public readonly static Error NoTePertenece = new Error("notificacion.no_te_pertenece", "No te pertenece esta notificación");
    }

    public abstract class HiloInteraccionNotificacion : Notificacion
    {
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }
        protected HiloInteraccionNotificacion() { }
        public HiloInteraccionNotificacion(UsuarioId usuarioId, HiloId hiloId, ComentarioId comentarioId) : base(usuarioId)
        {
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }
    }

    public class HiloComentadoNotificacion : HiloInteraccionNotificacion
    {
        private HiloComentadoNotificacion() { }
        public HiloComentadoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId) : base(usuarioId, hilo, comentarioId) { }
    }

    public class HiloSeguidoNotificacion : HiloInteraccionNotificacion
    {
        private HiloSeguidoNotificacion() { }
        public HiloSeguidoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId) : base(usuarioId, hilo, comentarioId) { }
    }
    public class ComentarioRespondidoNotificacion : HiloInteraccionNotificacion
    {
        public ComentarioId RespondidoId { get; private set; }
        private ComentarioRespondidoNotificacion() { }
        public ComentarioRespondidoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId, ComentarioId respondidoId) : base(usuarioId, hilo, comentarioId)
        {
            RespondidoId = respondidoId;
        }
    }
}