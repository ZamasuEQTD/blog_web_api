using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Comentarios.GetComentarioDeHilos;
using SharedKernel;
using Dapper;
using Application.Features.Hilos.Queries.GetHilo;
using Application.Abstractions;
namespace Application.Comentarios.GetComentariosDeHilo;

public class GetComentariosDeHiloQueryHandler : IQueryHandler<GetComentariosDeHiloQuery, List<GetComentarioResponse>>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;

    public GetComentariosDeHiloQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<List<GetComentarioResponse>>> Handle(GetComentariosDeHiloQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"
            SELECT 
                comentario.id,
                comentario.created_at as createdat,
                comentario.texto,
                comentario.autor_id as autorid,
                comentario.color,
                hilo.autor_id = comentario.autor_id AS EsOp,
                comentario.autor_id = @UsuarioId AS EsAutor,
                CASE
                    WHEN comentario.autor_id = @UsuarioId THEN comentario.recibir_notificaciones
                END AS RecibirNotificaciones,
                NULL as destacado_at,  
                FALSE as destacado,
                comentario.autor_nombre as nombre,
                comentario.autor_rango as rango,
                comentario.tag,
                comentario.dados,
                comentario.tag_unico as tagunico,
                portada.url,
                portada.previsualizacion,
                spoiler.spoiler,
                portada.provider
            FROM comentarios comentario
            JOIN hilos hilo ON hilo.id = comentario.hilo_id
            LEFT JOIN medias_spoileables spoiler ON comentario.media_spoileable_id = spoiler.id
            LEFT OUTER JOIN medias portada ON spoiler.hashed_media_id = portada.id
            WHERE 
                comentario.hilo_id = @HiloId 
            AND 
                comentario.status = 'Activo'
            AND 
                (@UltimoComentario IS NULL OR comentario.created_at < (SELECT created_at FROM comentarios WHERE id = @UltimoComentario)
            AND (
                @UsuarioId IS NULL OR comentario.id NOT IN (
                    SELECT 
                        comentario_id
                    FROM comentarios_interacciones 
                    WHERE usuario_id = @UsuarioId AND oculto
                    )
                )
            ORDER BY destacado_at DESC, createdat DESC
            LIMIT 20
        ";

        using var connection = _connection.CreateConnection();

        IEnumerable<GetComentarioResponse> comentarios = await connection.QueryAsync<GetComentarioResponse, GetHiloAutorResponse, GetComentarioDetallesResponse,GetHiloMediaResponse?, GetComentarioResponse>(
        sql, 
        (comentario, autor, detalles,media) => {
            comentario.Autor = autor;
            
            comentario.Detalles = detalles;
            
            comentario.Media = media;

            return comentario;
        },
        param: new {
            request.HiloId,
            request.UltimoComentario,
            _user.UsuarioId
        }, 
        splitOn: "nombre,tag"
        );

        return comentarios.ToList();
    }
}