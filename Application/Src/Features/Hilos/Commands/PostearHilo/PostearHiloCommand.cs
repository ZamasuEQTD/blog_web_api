using Application.Abstractions.Messaging;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommand : ICommand<Guid>
    {
        public required string Titulo { get; set; }
        public required string Descripcion { get; set; }
        public required bool Spoiler { get; set; }
        public required Guid Subcategoria { get; set; }
        public List<string> Encuesta { get; set; } = [];
        public required bool DadosActivados { get; set; }
        public required bool IdUnicoAtivado { get; set; }
    }

}