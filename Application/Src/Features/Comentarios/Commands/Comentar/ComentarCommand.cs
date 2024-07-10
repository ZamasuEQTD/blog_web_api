using Application.Abstractions.Messaging;

namespace Application.Comentarios.Commands
{
    public class ComentarCommand : ICommand {
        public string Texto {get; set;}
        public Guid HiloId {get; set;}
        public ComentarCommand(Guid hiloId, string texto)
        {
            HiloId = hiloId;
            Texto = texto;
        }
    }
}