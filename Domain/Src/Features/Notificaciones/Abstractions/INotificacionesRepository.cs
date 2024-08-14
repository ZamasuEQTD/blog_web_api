using Domain.Usuarios;

namespace Domain.Notificaciones.Abstractions
{
    public interface INotificacionesRepository
    {
        void Add(Notificacion notificacion);
        Task<List<Notificacion>> GetNotificaciones(UsuarioId id);
        Task<Notificacion> GetNotificacion(NotificacionId id);
    }
}