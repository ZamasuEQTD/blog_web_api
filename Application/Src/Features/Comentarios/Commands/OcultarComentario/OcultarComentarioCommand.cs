using Application.Abstractions.Messaging;

namespace Application.Comentarios.Commands
{
    public class OcultarComentarioCommand : ICommand {
        public Guid Comentario { get; set; }
        public Guid Hilo { get; set; }
    }
}