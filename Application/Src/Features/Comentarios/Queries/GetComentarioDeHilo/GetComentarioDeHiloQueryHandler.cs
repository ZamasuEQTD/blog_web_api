using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Comentarios.GetComentarioDeHilos;
using Application.Features.Comentarios.Queries.GetComentarioDeHilo;
using Application.Features.Hilos.Queries.GetHilo;
using Dapper;
using Domain.Comentarios;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Comentarios.GetComentariosDeHilo;

public class GetComentarioDeHiloQueryHandler : IQueryHandler<GetComentarioDeHiloQuery, GetComentarioResponse>
{

    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;

    public GetComentarioDeHiloQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<GetComentarioResponse>> Handle(GetComentarioDeHiloQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"
            SELECT 
                comentario.id,
                comentario.created_at as createdat,
                comentario.texto,
                comentario.autor_id as autorid,
                comentario.color,
                hilo.autor_id = comentario.autor_id AS EsOp,
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
                comentario.tag = @tag
            AND 
                comentario.status = 'Activo'
        ";

        List<string> roles = _user.IsAuthenticated? _user.Roles : [];

        using var connection =  _connection.CreateConnection();
    

        IEnumerable<GetComentarioResponse> comentarios = await connection.QueryAsync<GetComentarioResponse, GetHiloAutorResponse, GetComentarioDetallesResponse, GetHiloMediaResponse?, GetComentarioResponse>(
        sql, 
        (comentario, autor, detalles,media) => {
            comentario.Autor = autor;
            
            comentario.Detalles = detalles;
            
            comentario.Media = media;

            return comentario;
        },
        param: new {
            request.HiloId,
            request.Tag,
            UsuarioId = _user.IsAuthenticated? (Guid?) _user.UsuarioId : null
        }, 
        splitOn: "nombre,tag,url"
        );

        GetComentarioResponse? comentario = comentarios.FirstOrDefault();

        if(comentario is null) return ComentariosFailures.NoEncontrado;

        IEnumerable<string> respuestas = await connection.QueryAsync<string>(@"
            SELECT 
	            respuesta.tag
            FROM respuestas_comentarios interaccion
            JOIN comentarios respuesta ON respuesta.id = interaccion.respuesta_id
            WHERE interaccion.respondido_id = @Id
            ORDER by created_at
        ", 
        new {
            comentario.Id
        }
        );

        IEnumerable<string> responde = await connection.QueryAsync<string>(@"
            SELECT
	            respuesta.tag
            FROM respuestas_comentarios
            JOIN comentarios respuesta ON respuesta.id = respuesta_id
            WHERE respuesta_id = @Id
        ", new {
            comentario.Id,
            Roles =_user.IsAuthenticated? _user.Roles:[],
            _user.UsuarioId
        });

        comentario.Respuestas = respuestas.ToList();
        
        comentario.Responde = responde.ToList();

        if(!roles.Contains(Role.Moderador.Name!)){
            comentario.AutorId = null;
        }

        return comentario;
    }
}