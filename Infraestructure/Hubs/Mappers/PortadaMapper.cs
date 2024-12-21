using Application.Hilos.Queries.Responses;

namespace Infraestructure.Hubs.Mappers;

static class PortadaHubMapper
{
    public static GetHiloPortadaResponse ToAutor(GetHiloPortadaResponse portada)
    {
        return new GetHiloPortadaResponse{
            Id = portada.Id,
            Titulo = portada.Titulo,
            Descripcion = portada.Descripcion,
            AutorId = null,
            Miniatura = portada.Miniatura,
            Banderas = portada.Banderas,
            Subcategoria = portada.Subcategoria,
            EsOp = true,
            EsSticky = portada.EsSticky,
            RecibirNotificaciones = portada.RecibirNotificaciones
        };
    }

    public static GetHiloPortadaResponse ToModerador(GetHiloPortadaResponse portada)
    {
        return new GetHiloPortadaResponse{
            Id = portada.Id,
            AutorId = portada.AutorId,
            Titulo = portada.Titulo,
            Descripcion = portada.Descripcion,
            Miniatura = portada.Miniatura,
            Banderas = portada.Banderas,
            Subcategoria = portada.Subcategoria,
            EsOp = false,
            EsSticky = false,
            RecibirNotificaciones = null
        };
    }

    public static GetHiloPortadaResponse ToAnonimo(GetHiloPortadaResponse portada)
    {
        return new GetHiloPortadaResponse{
            Id = portada.Id,
            AutorId = null,
            Titulo = portada.Titulo,
            Descripcion = portada.Descripcion,
            Miniatura = portada.Miniatura,
            Banderas = portada.Banderas,
            Subcategoria = portada.Subcategoria,
            EsOp = false,
            EsSticky = false,
            RecibirNotificaciones = null
        };
    }
}
