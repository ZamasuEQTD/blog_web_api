using System;
using Application.Abstractions.Messaging;
using Application.Comentarios.GetComentarioDeHilos;

namespace Application.Features.Comentarios.Queries.GetComentarioDeHilo
{
    public class GetComentarioDeHiloQuery : IQuery<GetComentarioResponse>
    {
        public Guid HiloId { get; set; }
        public string Tag { get; set; }

        public GetComentarioDeHiloQuery(Guid hiloId, string tag)
        {
            HiloId = hiloId;
            Tag = tag;
        }
    }
}
