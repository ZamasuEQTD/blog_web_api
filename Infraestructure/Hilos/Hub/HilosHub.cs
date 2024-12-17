using Application.Abstractions.Data;
using Application.Features.Hilos.Abstractions;
using Application.Features.Hilos.Queries.Responses;
using Application.Hilos.Queries.Responses;
using Dapper;
using Infraestructure.Hub;
using Microsoft.AspNetCore.SignalR;

namespace Infraestructure.Hilos;

 
public class ConnectionManager<T>
{
    public Dictionary<string, UserHubConnection?> Connections { get; set; } = [];
    public void RemoveConnection(string connectionId)
    {
        Connections.Remove(connectionId);
    }

    public void AddConnection(string connectionId, UserHubConnection? user){
        this.Connections[connectionId] = user;
    }

}

public class HilosHubService : IHilosHubService
{
    private readonly IHubContext<HilosHub> _hubContext;
    private readonly IDBConnectionFactory _connection;
    private readonly ConnectionManager<HilosHub> _connectionManager;  

    public HilosHubService(IHubContext<HilosHub> hubContext, IDBConnectionFactory connection, ConnectionManager<HilosHub> connectionManager)
    {
        _hubContext = hubContext;
        _connection = connection;
        _connectionManager = connectionManager;
    }

    public async Task NotificarHiloPosteado(Guid id)
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
            new { Id = id },
            splitOn: "DadosActivados,spoiler"
        );

        GetHiloPortadaResponse? portada = portadas.FirstOrDefault();

        if (portada is null) return;

        List<Task> tasks = [];

        foreach (var userConnection in _connectionManager.Connections)
        {
            GetHiloPortadaResponse response;

            if (userConnection.Value?.UserId == portada.AutorId?.ToString())
                response = PortadaHubMapper.ToAutor(portada);
            else if (userConnection.Value?.UserType == UserType.Moderador)
                response = PortadaHubMapper.ToModerador(portada);
            else
                response = PortadaHubMapper.ToAnonimo(portada);

            tasks.Add(_hubContext.Clients.Client(userConnection.Key).SendAsync("OnHiloPosteado", response));
        }

        await Task.WhenAll(tasks);
    }
    public async Task OnHiloEliminado(Guid id) =>  await _hubContext.Clients.All.SendAsync("OnHiloEliminado", id);
}

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

public interface IHilosHub
{
    Task OnHiloPosteado(string hilo);
}

public class HilosHub : Hub<IHilosHub>
{
    private readonly ConnectionManager<HilosHub> _connectionManager;
    public HilosHub(ConnectionManager<HilosHub> connectionManager)
    {
        _connectionManager = connectionManager;
    }
    public override async Task OnConnectedAsync()
    {
        _connectionManager.AddConnection(Context.ConnectionId,null);

        await base.OnConnectedAsync();
    }
}