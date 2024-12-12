using Application.Abstractions.Messaging;
using Application.Hilos.Queries.Responses;

namespace Application.Features.Hilos.Queries.GetColeccion;

public class GetColeccionQuery : IQuery<IEnumerable<GetHiloPortadaResponse>>
{
    public Guid? UltimoHilo { get; set; }
    public TipoDeColeccion Tipo { get; set; }
}

public enum TipoDeColeccion
{
    Favoritos,
    Ocultos,
    Seguidos
}
