using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using MediatR;
using SharedKernel;

namespace Application.Notificaciones.Queries;

public class GetNotificacionesQueryHandler : IQueryHandler<GetNotificacionesQuery, IEnumerable<GetNotificacionResponse>>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _userContext;
    public GetNotificacionesQueryHandler(IDBConnectionFactory connection, IUserContext userContext)
    {
        _connection = connection;   
        _userContext = userContext;
    }

    public async Task<Result<IEnumerable<GetNotificacionResponse>>> Handle(GetNotificacionesQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connection.CreateConnection();
        
        IEnumerable<GetNotificacionResponse> notificaciones = await connection.QueryAsync<GetNotificacionResponse, GetHiloNotificacionResponse, GetNotificacionResponse>(
            @"
            SELECT 
                notificacion.tipo,
                notificacion.id,
                notificacion.created_at as fecha,
                notificacion.comentario_id as comentario_id,
                CASE
                    WHEN notificacion.tipo = 'hilo_comentado' OR notificacion.tipo = 'hilo_seguido_comentado' THEN h.titulo
                    WHEN notificacion.tipo = 'comentario_respondido' THEN c.texto
                    ELSE NULL
                END AS contenido,
                h.id as id,
                h.titulo as titulo,
                p.hash as hash
            FROM notificaciones notificacion
            JOIN hilos h ON notificacion.hilo_id = h.id
            JOIN comentarios c ON notificacion.comentario_id = c.id
            JOIN media p ON h.portada_id = p.id
            WHERE notificacion.usuario_id = @UsuarioId AND notificacion.status = 'Activo' AND notificacion.created_at > @UltimaNotificacion
            ORDER BY fecha DESC
            LIMIT 20
            ",
            (notificacion, hilo) =>
            {
                hilo.Imagen = "/media/thumbnails/" + hilo.Hash + ".jpeg";
                
                notificacion.Hilo = hilo;

                return notificacion;
            },
            new { _userContext.UsuarioId, request.UltimaNotificacion }
        );

        return Result.Success(notificaciones);
    }
}

