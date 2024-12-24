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
        public bool Leida { get; private set; }
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }

        protected Notificacion() { }
        protected Notificacion(UsuarioId usuarioId, HiloId hiloId, ComentarioId comentarioId)
        {
            this.Id = new(Guid.NewGuid());
            this.NotificadoId = usuarioId;
            this.Leida = false;
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }

        public Result Leer(UsuarioId usuarioId)
        {
            if (usuarioId != NotificadoId) return NotificacionesFailures.NoTePertenece;

            this.Leida = true;

            return Result.Success();
        }

        public bool EsUsuarioNotificado(UsuarioId usuarioId) => this.NotificadoId == usuarioId;
    }   

     

    public class NotificacionId : EntityId
    {
        public NotificacionId(Guid id) : base(id) { }
    }

    static public class NotificacionesFailures
    {
        public readonly static Error NoTePertenece = new Error("notificacion.no_te_pertenece", "No te pertenece esta notificación");
    }

    public class HiloComentadoNotificacion : Notificacion
    {
        private HiloComentadoNotificacion() { }
        public HiloComentadoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId) : base(usuarioId, hilo, comentarioId) { }
    }

    public class HiloSeguidoNotificacion : Notificacion
    {
        private HiloSeguidoNotificacion() { }
        public HiloSeguidoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId) : base(usuarioId, hilo, comentarioId) { }
    }
    public class ComentarioRespondidoNotificacion : Notificacion
    {
        public ComentarioId RespondidoId { get; private set; }
        private ComentarioRespondidoNotificacion() { }
        public ComentarioRespondidoNotificacion(UsuarioId usuarioId, HiloId hilo, ComentarioId comentarioId, ComentarioId respondidoId) : base(usuarioId, hilo, comentarioId)
        {
            RespondidoId = respondidoId;
        }
    }
}