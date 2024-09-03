using Application.Abstractions.Messaging;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public string Texto { get; set; }
        public ComentarHiloCommand(Guid hilo, string texto)
        {
            Hilo = hilo;
            Texto = texto;
        }

    }
}