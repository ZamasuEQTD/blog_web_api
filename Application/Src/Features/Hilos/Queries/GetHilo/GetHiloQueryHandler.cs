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
                CASE
                    WHEN true THEN hilo.autor_id
                ELSE NULL
                END AS AutorId,
                hilo.created_at AS CreatedAt,
                @UsuarioId = hilo.autor_id AS EsOp,
                CASE
                    WHEN hilo.autor_id = @UsuarioId THEN hilo.recibir_notificaciones
                ELSE NULL
                END AS RecibirNotificaciones,
                (
	                SELECT
	                	count(c.id)
	                FROM comentarios c
	                WHERE c.hilo_id = hilo.id
	            )
                AS CantidadComentarios,
                hilo.dados AS DadosActivados,
                hilo.id_unico_activado AS IdUnicoActivado,
                hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
                portada.url,
                portada.previsualizacion,
                spoiler.spoiler,
                hilo.autor_nombre as Nombre,
                hilo.autor_rango as Rango,
                subcategoria.id,
                subcategoria.nombre
            FROM hilos hilo
            JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
            JOIN medias_spoileables spoiler ON hilo.portada_id = spoiler.id
            JOIN medias portada ON spoiler.hashed_media_id = portada.id
            LEFT JOIN stickies sticky ON hilo.id = sticky.hilo_id
            WHERE hilo.id = @hilo;
        ";
        
        using var connection = _connection.CreateConnection();

        var result = await  connection.QueryAsync<GetHiloResponse,GetHiloBanderasResponse,GetHiloMediaResponse,GetHiloAutorResponse,GetSubcategoriaResponse  ,GetHiloResponse  >(sql, 
            (hilo,banderas,media,autor,subcategoria) => {
                hilo.Subcategoria = subcategoria;
                hilo.Banderas = banderas;
                hilo.Media = media;
                hilo.Autor = autor;
                return hilo;
            },
            new { 
                hilo = request.Hilo,
                UsuarioId = _user.IsLogged ? (Guid?) _user.UsuarioId : null 
            }
            ,splitOn: "DadosActivados,url,nombre,id"
        );

        GetHiloResponse? hilo = result.FirstOrDefault();

        if (hilo == null) return HilosFailures.NoEncontrado;

        return hilo;
    }

}
