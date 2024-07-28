using Application.Abstractions.Messaging;

namespace Application.Comentarios.Commands {
    public class EliminarComentarioCommand : ICommand {
        public Guid Comentario {get; set;}
        public Guid Hilo {get; set;}
        public EliminarComentarioCommand(Guid comentario, Guid hilo)
        {
            Comentario = comentario;
            Hilo = hilo;
        }
    }
}