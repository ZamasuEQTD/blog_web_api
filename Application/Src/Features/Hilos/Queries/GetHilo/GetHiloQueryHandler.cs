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

    public GetHiloQueryHandler(IDBConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"
            SELECT
                hilo.id,
                hilo.titulo,
                hilo.descripcion,
                CASE
                    WHEN @IsLogged THEN hilo.usuario_id
                ELSE NULL
                END AS AutorId,
                hilo.created_at AS CreatedAt,
                @UsuarioId = hilo.usuario_id AS EsOp,
                CASE
                    WHEN @IsLogged AND hilo.usuario_id = @UsuarioId THEN hilo.recibir_notificaciones
                ELSE NULL
                END AS RecibirNotificaciones,
                (
	                SELECT
	                	count(c.id)
	                FROM comentarios c
	                WHERE c.hilo_id = hilo.id
	            )
                AS Comentarios,
                hilo.dados AS DadosActivados,
                hilo.id_unico_activado AS IdUnicoActivado,
                hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
                portada.url,
                portada.previsualizacion,
                spoiler.spoiler,
                hilo.autor_nombre as Nombre,
                hilo.rango,
                hilo.rango_corto as RangoCorto,
                subcategoria.id,
                subcategoria.nombre
            FROM hilos hilo
            JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
            JOIN media_spoileables spoiler ON hilo.portada_id = spoiler.id
            JOIN media portada ON spoiler.media_id = portada.id
            WHERE hilo.id = @hilo;
        ";
        using var connection = _connection.CreateConnection();

        GetHiloResponse? hilo;

        var result = await  connection.QueryAsync<GetHiloResponse,GetHiloBanderasResponse,GetHiloMediaResponse,GetHiloAutorResponse,GetSubcategoriaResponse  ,GetHiloResponse  >(sql, 
            (hilo,banderas,media,autor,subcategoria) => {
                hilo.Subcategoria = subcategoria;
                hilo.Banderas = banderas;
                hilo.Media = media;
                hilo.Autor = autor;
                return hilo;
            },
            new { hilo = request.Hilo }
            ,splitOn: "DadosActivados,url,nombre,id"
        );

        return result.FirstOrDefault();
    }

}
