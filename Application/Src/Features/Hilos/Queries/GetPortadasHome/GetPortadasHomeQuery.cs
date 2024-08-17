using Application.Abstractions.Messaging;

namespace Application.Hilos.Queries
{
    public class GetPortadasHomeQuery : IQuery<List<GetPortadaHomeResponse>>
    {
        public string? Titulo { get; set; }
        public DateTime? UltimoBump { get; set; }
        public Guid? Categoria { get; set; }
    }

    public class GetPortadaHomeResponse
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool Spoiler { get; set; }
        public string Image { get; set; }
        public List<string> Banderas { get; set; } = ["tag_unico", "encuesta", "dice"];
    }

    public class GetAttachDto
    {
        public string ContentType { get; set; }
        public string Url { get; set; }
        public string Type { get; set; }
        public string Host { get; set; }
    }
}