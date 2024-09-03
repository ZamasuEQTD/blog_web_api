using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class EstablecerStickyCommand : ICommand
    {
        public Guid Hilo { get; set; }

        public EstablecerStickyCommand(Guid hilo)
        {
            Hilo = hilo;
        }
    }
}