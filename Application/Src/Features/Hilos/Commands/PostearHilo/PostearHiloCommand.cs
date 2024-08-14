using Application.Abstractions.Messaging;
using Application.Medias.Services;
using static Application.Medias.Services.FileMediaProcesor;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommand : ICommand<Guid>
    {
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required IFileProvider? PortadaFile { get; set; }
        public required IUrlProvider? PortadaUrl { get; set; }
        public required bool Spoiler { get; set; }
        public required Guid Subcategoria { get; set; }
        public List<string> Encuesta { get; set; } = [];
        public bool DadosActivados { get; set; }
        public bool IdUnicoAtivado { get; set; }
    }

}