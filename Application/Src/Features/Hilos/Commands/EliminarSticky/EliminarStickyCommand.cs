using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class EliminarStickyCommand : ICommand
    {
        public Guid Hilo {get; set;}
    }
}