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
                        h.descripcion as contenido,
                        h.created_at as fecha,
                        h.id,
                        h.titulo,
                        portada.hash
                    FROM hilos h
                    JOIN medias portada ON p.id h = h.portada_id
                    WHERE h.status = 'Activo' AND h.created_at > @UltimoHilo AND h.autor_id = @Usuario
                    ORDER BY h.created_at DESC
                    LIMIT 20";

            IEnumerable<GetRegistroDeHiloResponse> registros = await connection.QueryAsync<GetRegistroDeHiloResponse, GetHiloRegistroResponse, GetRegistroDeHiloResponse  >(sql,
                (comentario, hilo) =>
                {
                    hilo.Imagen = "/media/thumbnails/" + hilo.Hash + ".jpeg";
                    
                    return new GetRegistroDeHiloResponse
                    {
                        Contenido = comentario.Contenido,
                        Fecha = comentario.Fecha,
                        Hilo = hilo
                    };
                }
                ,splitOn: "Id"
                );
            
            return registros.ToList();
        }
    }
}