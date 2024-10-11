using Application.Abstractions.Messaging;
using Domain.Media.Abstractions;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public string Texto { get; set; }
        public IEmbedFile? EmbedFile { get; set; }
        public IFile? File { get; set; }

        public ComentarHiloCommand(Guid hilo, string texto, IEmbedFile? embedFile, IFile? file)
        {
            Hilo = hilo;
            Texto = texto;
            EmbedFile = embedFile;
            File = file;
        }

    }
}