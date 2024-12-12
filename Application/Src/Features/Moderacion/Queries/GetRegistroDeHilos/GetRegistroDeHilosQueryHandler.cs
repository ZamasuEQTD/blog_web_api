using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Moderacion.Queries;

public class GetRegistroDeHilosQueryHandler : IQueryHandler<GetRegistroDeHilosQuery, IEnumerable<GetRegistroDeHiloResponse>>
{
    private readonly IDBConnectionFactory _connection;
    public GetRegistroDeHilosQueryHandler(IDBConnectionFactory connection)
    {
        _connection = connection;
    }

    public async Task<Result<IEnumerable<GetRegistroDeHiloResponse>>> Handle(GetRegistroDeHilosQuery request, CancellationToken cancellationToken)
    {
        using (var connection = _connection.CreateConnection())
        {
            var sql = @"
                    SELECT
                        h.descripcion AS contenido,
                        h.created_at AS fecha,
                        h.id,
                        h.titulo,
                        portada.provider,
                        portada.miniatura
                    FROM hilos h
                    JOIN medias_spoileables spoiler ON spoiler.id = h.portada_id
 					JOIN medias portada ON portada.id = spoiler.hashed_media_id
                    WHERE h.status = 'Activo' AND h.autor_id = @Usuario AND (@UltimoHilo IS NULL OR h.created_at < (
                        SELECT 
                            created_at 
                        FROM hilos 
                        WHERE id = @UltimoHilo
                        )
                    )
                    ORDER BY h.created_at DESC
                    LIMIT 20
            ";

            IEnumerable<GetRegistroDeHiloResponse> registros = await connection.QueryAsync<GetRegistroDeHiloResponse, GetHiloRegistroResponse, GetHiloMiniaturaResponse, GetRegistroDeHiloResponse  >(sql,
                (registro, hilo, miniatura) =>
                {
                    hilo.Miniatura = miniatura;

                    registro.Hilo = hilo;
                    
                    return registro;
                }
                ,
                splitOn: "Id, provider",
                param: new {
                    request.UltimoHilo,
                    request.Usuario
                }
            );
            
            return registros.ToList();
        }
    }
}