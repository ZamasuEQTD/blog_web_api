using Application.Abstractions.Messaging;

namespace Application.Comentarios
{
    public class GetComentariosDeHiloQuery : IQuery<List<GetComentarioResponse>>
    {
        public Guid Hilo { get; set; }
        public Guid? UltimoComentario { get; set; }
    }

    public class GetComentarioResponse
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Texto { get; set; }
        public string Tag { get; set; }
        public string? TagUnico { get; set; }
        public string? Dados { get; set; }
    }

    public class GetComentarioRe
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Texto { get; set; }
    }

    public class GetBanderasDeComentarioResponse
    {
        public string Tag { get; set; }
        public string TagUnico { get; set; }
        public string Dados { get; set; }
    }
}