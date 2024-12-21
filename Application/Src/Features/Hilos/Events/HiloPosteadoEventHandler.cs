using Application.Abstractions.Data;
using Application.Features.Hilos.Abstractions;
using Application.Features.Hilos.Queries.Responses;
using Application.Hilos.Queries.Responses;
using Dapper;
using Domain.Hilos.DomainEvents;
using MediatR;

namespace Application.Hilos.Events ;

public class HiloPosteadoEventHandler : INotificationHandler<HiloPosteadoDomainEvent>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IHomeHubService _hub;
    public HiloPosteadoEventHandler(IDBConnectionFactory connection, IHomeHubService hub)
    {
        _connection = connection;
        _hub = hub;
    }

    public async Task Handle(HiloPosteadoDomainEvent notification, CancellationToken cancellationToken)
    {
        var sql = @$"
            SELECT 
                hilo.id,
                hilo.titulo,
                hilo.descripcion,
                hilo.created_at AS CreatedAt,
                hilo.ultimo_bump AS UltimoBump,
                hilo.recibir_notificaciones AS RecibirNotificaciones,
                subcategoria.nombre_corto AS Subcategoria,
                hilo.autor_id AS AutorId,
                false AS EsSticky,
                hilo.dados AS DadosActivados,
                hilo.id_unico_activado AS IdUnicoActivado,
                hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
                spoileable.spoiler,
                portada.miniatura AS Url,
                portada.provider AS Provider
            FROM hilos hilo
            JOIN medias_spoileables spoileable ON spoileable.id = hilo.portada_id
            JOIN medias portada ON portada.id = spoileable.hashed_media_id
            JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
            WHERE hilo.id = @Id
        ";

        using var connection = _connection.CreateConnection();

        var portadas = await connection.QueryAsync<GetHiloPortadaResponse, GetHiloBanderasResponse, GetHiloPortadaImagenResponse,GetHiloPortadaResponse>(sql,
            (portada, banderas,imagen ) => {
                portada.Banderas = banderas;

                portada.Miniatura = imagen;

                return portada;
            },
            new { Id = notification.HiloId.Value },
            splitOn: "DadosActivados,spoiler"
        );

        GetHiloPortadaResponse? hilo = portadas.FirstOrDefault();

        if (hilo is null) return;
        
        List<Task> tasks = [];

        if(hilo is null){
            return;
        }

        await _hub.NotificarHiloPosteado(hilo);
    }
}