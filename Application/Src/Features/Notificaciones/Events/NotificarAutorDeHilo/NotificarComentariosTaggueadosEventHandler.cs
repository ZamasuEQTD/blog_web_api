using Application.Abstractions.Data;
using Domain.Comentarios;
using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos.Events;
using Domain.Notificaciones;
using Domain.Notificaciones.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using MediatR;

namespace Application.Notificaciones.Events
{
    public class NotificarComentariosTaggueadosEventHandler : INotificationHandler<HiloComentadoDomainEvent>
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly INotificacionesRepository _notificacionesRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public NotificarComentariosTaggueadosEventHandler(IUnitOfWork unitOfWork, IComentariosRepository comentariosRepository, IUsuariosRepository usuariosRepository, INotificacionesRepository notificacionesRepository)
        {
            _unitOfWork = unitOfWork;
            _comentariosRepository = comentariosRepository;
            _usuariosRepository = usuariosRepository;
            _notificacionesRepository = notificacionesRepository;
        }

        public async Task Handle(HiloComentadoDomainEvent notification, CancellationToken cancellationToken)
        {
            Comentario? comentario = await _comentariosRepository.GetComentarioById(notification.ComentarioId);

            if (comentario is null) return;

            List<string> tags = TagUtils.GetTags(comentario.Texto.Value);

            foreach (var tag in tags)
            {

                Tag _tag = Tag.Create(tag).Value;

                Comentario? taggueado = await _comentariosRepository.GetComentarioByTag(notification.HiloId, _tag);

                if (taggueado is not null)
                {
                    _notificacionesRepository.Add(new ComentarioRespondidoNotificacion(
                        taggueado.AutorId,
                        notification.HiloId,
                        notification.ComentarioId,
                        taggueado.Id
                    ));
                }
            }

            await _unitOfWork.SaveChangesAsync();
        }
    }
}