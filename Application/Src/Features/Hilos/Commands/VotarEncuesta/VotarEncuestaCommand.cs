using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands {
    public class VotarEncuestaCommand : ICommand {
        public Guid Hilo {get ; private set;}
        public Guid Respuesta {get ; private set;}
    }
}