using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class EliminarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }

        public EliminarHiloCommand(Guid hilo)
        {
            Hilo = hilo;
        }
    }
}