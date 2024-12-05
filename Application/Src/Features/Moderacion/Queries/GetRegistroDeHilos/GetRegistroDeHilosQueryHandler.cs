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
                        portada.hash
                    FROM hilos h
                    JOIN media_references portada_ref ON portada_ref.id = h.portada_id
 					JOIN media portada ON portada.id = portada_ref.media_id
                    WHERE h.status = 'Activo' AND h.created_at > @UltimoHilo AND h.autor_id = @Usuario
                    ORDER BY h.created_at DESC
                    LIMIT 20
            ";

            IEnumerable<GetRegistroDeHiloResponse> registros = await connection.QueryAsync<GetRegistroDeHiloResponse, GetHiloRegistroResponse, GetRegistroDeHiloResponse  >(sql,
                (registro, hilo) =>
                {
                    hilo.Imagen = "/media/thumbnails/" + hilo.Hash + ".jpeg";
                    
                    return new GetRegistroDeHiloResponse
                    {
                        Contenido = registro.Contenido,
                        Fecha = registro.Fecha,
                        Hilo = hilo
                    };
                }
                ,
                splitOn: "Id",
                param: new {
                    request.UltimoHilo,
                    request.Usuario
                }
            );
            
            return registros.ToList();
        }
    }
}