
using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands {
    public class VotarEncuestaCommand : ICommand
    {
        public Guid HiloId { get; set; }
        public Guid RespuestaId { get; set; }
    }
}