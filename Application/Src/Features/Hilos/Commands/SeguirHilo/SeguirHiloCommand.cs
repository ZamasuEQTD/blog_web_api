using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class SeguirHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
    }
}