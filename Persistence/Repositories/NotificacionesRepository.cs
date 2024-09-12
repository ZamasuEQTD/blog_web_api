using Domain.Notificaciones;
using Domain.Notificaciones.Abstractions;
using Domain.Usuarios;

namespace Persistence.Repositories
{
    public class NotificacionesRepository : INotificacionesRepository
    {
        public void Add(Notificacion notificacion)
        {
            throw new NotImplementedException();
        }

        public Task<Notificacion> GetNotificacion(NotificacionId id)
        {
            throw new NotImplementedException();
        }

        public Task<List<Notificacion>> GetNotificaciones(UsuarioId id)
        {
            throw new NotImplementedException();
        }
    }
}