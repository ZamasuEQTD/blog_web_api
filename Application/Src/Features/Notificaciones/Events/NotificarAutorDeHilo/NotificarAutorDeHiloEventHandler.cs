using Application.Abstractions.Data;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Events;
using Domain.Notificaciones;
using Domain.Notificaciones.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using MediatR;

namespace Application.Hilos.Events
{
    public class NotificarAutorDeHiloEventHandler : INotificationHandler<HiloComentadoDomainEvent>
    {
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly IHilosRepository _hilosRepository;
        private readonly IUnitOfWork _unitOfWork;
        public NotificarAutorDeHiloEventHandler(IUnitOfWork unitOfWork, IHilosRepository hilosRepository, INotificacionesRepository notificacionesRepository)
        {
            _unitOfWork = unitOfWork;
            _hilosRepository = hilosRepository;
            _notificacionesRepository = notificacionesRepository;
        }

        public async Task Handle(HiloComentadoDomainEvent notification, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(notification.HiloId);

            _notificacionesRepository.Add(new HiloComentadoNotificacion(
                hilo!.AutorId,
                hilo.Id,
                notification.ComentarioId
            ));

            await _unitOfWork.SaveChangesAsync();
        }
    }
}