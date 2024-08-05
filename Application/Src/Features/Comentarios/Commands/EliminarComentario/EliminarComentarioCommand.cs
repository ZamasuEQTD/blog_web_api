using Application.Abstractions.Messaging;

namespace Application.Comentarios.Commands
{
    public class EliminarComentarioCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public Guid Comentario { get; set; }
    }
}