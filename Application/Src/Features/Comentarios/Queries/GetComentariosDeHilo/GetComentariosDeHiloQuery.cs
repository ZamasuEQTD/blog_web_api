using Application.Abstractions.Messaging;

namespace Application.Comentarios
{
    public class GetComentariosDeHiloQuery : IQuery<List<GetComentarioResponse>>
    {
        public Guid Hilo { get; set; }
        public DateTime? UltimoComentario { get; set; }
    }

    public class GetComentarioResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool Destacado { get; set; }
        public bool EsAutor { get; set; }
        public Guid? AutorId { get; set; }
        public bool? RecibirNotificaciones { get; set; }
        public string Texto { get; set; }
        public string Tag { get; set; }
        public string? TagUnico { get; set; }
        public string? Dados { get; set; }
    }
    public record GetComentarioQuery(
        Guid Id,
        DateTime CreatedAt,
        bool Destacado,
        Guid AutorId,
        bool RecibirNotificaciones,
        string Texto,
        string Tag,
        string? TagUnico,
        string? Dados
    )
    { }

    public class GetBanderasDeComentarioResponse
    {
        public string Tag { get; set; }
        public string TagUnico { get; set; }
        public string Dados { get; set; }
    }
}