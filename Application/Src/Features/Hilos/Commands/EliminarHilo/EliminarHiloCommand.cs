using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class EliminarHiloCommand : ICommand
    {
        public Guid HiloId { get; set; }

        public EliminarHiloCommand(Guid hiloId)
        {
            HiloId = hiloId;
        }
    }
}