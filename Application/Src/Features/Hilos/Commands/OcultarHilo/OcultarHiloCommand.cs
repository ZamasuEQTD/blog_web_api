using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands;
public class OcultarHiloCommand : ICommand
{
    public Guid Hilo { get; set; }
}