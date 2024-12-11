using Application.Abstractions.Messaging;
using Application.Hilos.Queries.Responses;

namespace Application.Features.Hilos.Queries.GetHiloPortada;

public class GetHiloPortadasQuery : IQuery<IEnumerable<GetHiloPortadaResponse>>
{
    public string? Titulo { get; set; }
    public DateTime? UltimoBump { get; set; }
    public Guid? Categoria { get; set; }
}