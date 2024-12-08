using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Features.Hilos.Queries.GetHiloPortada;
using Application.Features.Hilos.Queries.Responses;
using Application.Hilos.Queries.Responses;
using Dapper;
using SharedKernel;

namespace Application.Features.Hilos.Queries.GetHiloPortadas;

public class GetHiloPortadasQueryHandler : IQueryHandler<GetHiloPortadasQuery, IEnumerable<GetHiloPortadaResponse>>
{

    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;
    public GetHiloPortadasQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<IEnumerable<GetHiloPortadaResponse>>> Handle(GetHiloPortadasQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connection.CreateConnection();

        var sql = @$"(
        SELECT 
            hilo.id,
            hilo.titulo,
            hilo.descripcion,
            hilo.created_at as CreatedAt,
            hilo.ultimo_bump as UltimoBump,
            CASE
                WHEN hilo.usuario_id = @UsuarioId THEN hilo.recibir_notificaciones
            ELSE NULL
            END AS RecibirNotificaciones,
            subcategoria.nombre_corto AS Subcategoria,
            @IsLogged AND hilo.usuario_id = @UsuarioId AS EsOp,
            CASE
                WHEN @IsLogged THEN hilo.usuario_id
            ELSE NULL
            END AS AutorId,
            false AS EsSticky,
            hilo.dados AS DadosActivados,
            hilo.id_unico_activado AS IdUnicoActivado,
            hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
            spoileable.spoiler,
            portada.miniatura AS Url
        FROM hilos hilo
        JOIN media_spoileables spoileable ON spoileable.id = hilo.portada_id
        JOIN media portada ON portada.id = spoileable.media_id
        JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
        /**where**/
        {(_user.IsLogged ? 
        @"
        UNION ALL
        SELECT 
            hilo.id,
            hilo.titulo,
            hilo.descripcion,
            hilo.created_at as CreatedAt,
            hilo.ultimo_bump as UltimoBump,
            CASE
                WHEN AND hilo.usuario_id = @UsuarioId THEN hilo.recibir_notificaciones
            ELSE NULL
            END AS RecibirNotificaciones,
            subcategoria.nombre_corto AS Subcategoria,
            hilo.usuario_id = @UsuarioId AS EsOp,
            CASE
                WHEN @IsLogged THEN hilo.usuario_id
            ELSE NULL
            END AS AutorId,
            true AS EsSticky,
            hilo.dados AS DadosActivados,
            hilo.id_unico_activado AS IdUnicoActivado,
            hilo.encuesta_id IS NOT NULL AS TieneEncuesta,
            spoileable.spoiler,
            portada.miniatura AS url
        FROM hilos hilo
        JOIN media_spoileables spoileable ON spoileable.id = hilo.portada_id
        JOIN media portada ON portada.id = spoileable.media_id
        JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
        JOIN stickies sticky ON sticky.hilo_id = hilo.id
        /**where**/
        " : "")}
        )
        ORDER BY EsSticky DESC, UltimoBump DESC
        LIMIT 20
        ";
        
        SqlBuilder builder = new SqlBuilder();

        DynamicParameters parameters = new DynamicParameters();

        parameters.AddDynamicParams(new {
           _user.IsLogged,
            UsuarioId = _user.IsLogged ? (Guid?) _user.UsuarioId : null,
        });

        if(!string.IsNullOrEmpty(request.Titulo))
        {
            builder.Where("hilo.titulo ~ @Titulo", new { request.Titulo });
        }

        if(request.Categoria is not null ){
            builder.Where("hilo.subcategoria_id = @Categoria", new { request.Categoria });
        }

        if(request.UltimoBump != DateTime.MinValue) {
            Console.WriteLine("ultimo_bump: " + request.UltimoBump);

            builder.Where($"hilo.ultimo_bump < @ultimo_bump", new { ultimo_bump = request.UltimoBump });
        }

        if(_user.IsLogged){
            builder.Where($@"
                    hilo.id NOT IN (
                    SELECT
                        hilo_id
                    FROM hilo_interacciones
                    WHERE 
                        usuario_id = @UsuarioId
                    AND
                        oculto 
                    )
            ");
        }

        builder.AddParameters(parameters);

        SqlBuilder.Template template = builder.AddTemplate(sql);

        var portadas = await connection.QueryAsync<GetHiloPortadaResponse, GetHiloBanderasResponse, GetHiloPortadaImagenResponse,GetHiloPortadaResponse>(template.RawSql,
        (portada, banderas,imagen ) => {
            portada.Banderas = banderas;
    
            portada.Miniatura = imagen;

            return portada;
        },
        template.Parameters,
        splitOn: "DadosActivados,spoiler"
        );

        return Result.Success(portadas);
    }
}