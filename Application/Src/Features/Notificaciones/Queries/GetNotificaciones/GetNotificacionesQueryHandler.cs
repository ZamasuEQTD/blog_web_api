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
                notificacion.tipo_de_interaccion,
                notificacion.id,
                notificacion.created_at as fecha,
                notificacion.comentario_id as comentario_id,
                CASE 
                    WHEN notificacion.tipo_de_interaccion = 'hilo_comentado' OR notificacion.tipo_de_interaccion = 'hilo_seguido_comentado' THEN h.titulo
                    WHEN notificacion.tipo_de_interaccion = 'comentario_respondido' THEN c.texto
                END AS contenido,
                h.id as id,
                h.titulo as titulo,
                p.miniatura
            FROM notificaciones notificacion
            JOIN hilos h ON notificacion.hilo_id = h.id
            JOIN comentarios c ON notificacion.comentario_id = c.id
            JOIN media_spoileables spoiler ON h.portada_id = spoiler.id
            JOIN media p ON spoiler.media_id = p.id
            WHERE 
                notificacion.usuario_notificado_id = @UsuarioId
            AND 
                notificacion.status = 'SinLeer' 
            AND 
                notificacion.created_at < (
                    SELECT 
                        created_at
                    FROM notificaciones
                    WHERE id = @UltimaNotificacion
                )
            ORDER BY fecha DESC
            LIMIT 20
            ",
            (notificacion, hilo) =>
            {
                notificacion.Hilo = hilo;

                return notificacion;
            },
            new { _userContext.UsuarioId, request.UltimaNotificacion}
        );

        return Result.Success(notificaciones);
    }
}

