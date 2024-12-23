using Application.Abstractions.Messaging;
using Application.Medias.Abstractions;

namespace Application.Comentarios.Commands
{
    public class ComentarHiloCommand : ICommand
    {
        public Guid Hilo { get; set; }
        public string Texto { get; set; }
        public IEmbedFile? EmbedFile { get; set; }
        public IFile? File { get; set; }
        public bool Spoiler {get; set;}

        public ComentarHiloCommand(Guid hilo, string texto, IEmbedFile? embedFile, IFile? file, bool spoiler)
        {
            Hilo = hilo;
            Texto = texto;
            EmbedFile = embedFile;
            File = file;
            Spoiler = spoiler;
        }

    }
}