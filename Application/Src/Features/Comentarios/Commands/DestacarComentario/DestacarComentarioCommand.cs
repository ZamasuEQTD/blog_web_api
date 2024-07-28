using Application.Abstractions.Messaging;

namespace Domain.Comentarios.Commands
{
    public class DestacarComentarioCommand : ICommand {
        public Guid Hilo {get;set;}        
        public Guid Comentario {get;set;}        
    }
}