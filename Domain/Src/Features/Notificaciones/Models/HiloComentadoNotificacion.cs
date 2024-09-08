using Domain.Comentarios;
using Domain.Hilos;
using Domain.Notificaciones;
using Domain.Usuarios;

namespace Domain.Notificaciones
{
    public class HiloComentadoNotificacion : Notificacion
    {
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }
        private HiloComentadoNotificacion() : base() { }
        public HiloComentadoNotificacion(UsuarioId usuarioId, HiloId hiloId, ComentarioId comentarioId) : base(usuarioId)
        {
            HiloId = hiloId;
            ComentarioId = comentarioId;
        }
    }

    public class ComentarioRespondidoNotificacion : Notificacion
    {
        public HiloId HiloId { get; private set; }
        public ComentarioId ComentarioId { get; private set; }
        public ComentarioId ComentarioRespondidoId { get; private set; }
        public ComentarioRespondidoNotificacion(UsuarioId usuarioId, HiloId hiloId, ComentarioId comentarioId, ComentarioId comentarioRespondidoId) : base(usuarioId)
        {
            HiloId = hiloId;
            ComentarioId = comentarioId;
            ComentarioRespondidoId = comentarioRespondidoId;
        }
    }
}