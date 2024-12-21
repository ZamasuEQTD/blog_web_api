using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;
using Application.Features.Encuestas.Queries.Responses;
using Application.Features.Hilos.Queries.GetHilo;
using Application.Features.Hilos.Queries.Responses;
using Dapper;
using Domain.Comentarios;
using SharedKernel;

namespace Application.Hilos.Queries.GetHilo;

public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;
    public GetHiloQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"
        SELECT
            hilo.id,
            hilo.titulo,
            hilo.descripcion,
            sticky.id IS NOT NULL AS EsSticky,
            COUNT(comentario.id) AS cantidadcomentarios,
            CASE
                WHEN true THEN hilo.autor_id
            END AS AutorId,
            hilo.created_at AS CreatedAt,
            @UsuarioId = hilo.autor_id AS EsOp,
            CASE
                WHEN hilo.autor_id = @UsuarioId THEN hilo.recibir_notificaciones
            END AS RecibirNotificaciones,
            hilo.dados AS DadosActivados,
            hilo.id_unico_activado AS IdUnicoActivado,
            hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
            portada.url,
            portada.previsualizacion,
            spoiler.spoiler,
            portada.provider,
            hilo.autor_nombre as Nombre,
            hilo.autor_rango as Rango,
            subcategoria.id,
            subcategoria.nombre,
            hilo.encuesta_id AS id,
            CASE
                WHEN voto.votante_id = @UsuarioId THEN respuesta.id
            END AS RespuestaVotada,
            respuesta.id AS id,
            respuesta.contenido AS respuesta,
            count(voto.id) AS votos
        FROM hilos hilo
        JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
        JOIN medias_spoileables spoiler ON hilo.portada_id = spoiler.id
        JOIN medias portada ON spoiler.hashed_media_id = portada.id
        LEFT JOIN comentarios comentario ON hilo.id =  comentario.hilo_id
        LEFT JOIN stickies sticky ON sticky.hilo_id = hilo.id
        LEFT JOIN encuestas encuesta ON hilo.encuesta_id = encuesta.id
        LEFT OUTER JOIN respuestas respuesta ON encuesta.id = respuesta.encuesta_id
        LEFT OUTER JOIN votos voto ON respuesta.id = voto.respuesta_id
        WHERE
            hilo.id = @HiloId
        AND 
            hilo.status = 'Activo'
        GROUP BY
            hilo.id,
            respuesta.id,
            voto.votante_id,
            sticky.id,
            portada.id,
            spoiler.id,
            subcategoria.id
        ";
        
        using var connection = _connection.CreateConnection();

        GetEncuestaResponse? _encuesta = null;

        var result = await  connection.QueryAsync<GetHiloResponse,GetHiloBanderasResponse,GetHiloMediaResponse,GetHiloAutorResponse,GetSubcategoriaResponse, GetEncuestaResponse?,GetEncuestaRespuestaResponse?,GetHiloResponse  >(sql, 
            (hilo,banderas,media,autor,subcategoria, encuesta, respuesta) => {
                hilo.Subcategoria = subcategoria;
                hilo.Banderas = banderas;
                hilo.Media = media;
                hilo.Autor = autor;

                if(_encuesta is not null){
                    encuesta = _encuesta;
                } else {
                    _encuesta = encuesta;
                }

                if(respuesta is not null){
                    _encuesta!.Respuestas.Add(respuesta);
                }

                hilo.Encuesta = _encuesta;

                return hilo;
            },
            new { 
                HiloId = request.Hilo,
                UsuarioId = _user.IsLogged ? (Guid?) _user.UsuarioId : null 
            }
            ,splitOn: "DadosActivados,url,nombre,id,id,id"
        );

        GetHiloResponse? hilo = result.FirstOrDefault();

        if (hilo == null) return HilosFailures.NoEncontrado;

        return hilo;
    }

}
