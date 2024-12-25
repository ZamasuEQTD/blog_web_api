using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands;

public class CambiarNotificacionesHiloCommand :ICommand
{
    public Guid HiloId { get; set; }
}