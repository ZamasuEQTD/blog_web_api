using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class DenunciarHiloCommand : ICommand 
    {
        public Guid Hilo { get; set; }
    }
}