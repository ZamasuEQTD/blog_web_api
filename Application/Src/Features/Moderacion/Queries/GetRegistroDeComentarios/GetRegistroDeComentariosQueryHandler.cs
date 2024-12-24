using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Moderacion
{
    public class GetRegistroDeComentariosQueryHandler : IQueryHandler<GetRegistroDeComentariosQuery, List<GetRegistroDeComentarioResponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetRegistroDeComentariosQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetRegistroDeComentarioResponse>>> Handle(GetRegistroDeComentariosQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                var sql = @"
                    SELECT
                        c.id,
                        c.texto as contenido,
                        c.created_at as fecha,
                        h.id,
                        h.titulo,
                        portada.provider,
                        portada.miniatura
                    FROM comentarios c
                    JOIN hilos h ON h.id = c.hilo_id
                    JOIN medias_spoileables portada_ref ON portada_ref.id = h.portada_id
 					JOIN medias portada ON portada.id = portada_ref.hashed_media_id
                    WHERE h.status = 'Activo' AND c.status = 'Activo' AND h.autor_id = @Usuario AND (
                        @UltimoComentario IS NULL OR c.created_at < (
                            SELECT 
                                created_at 
                            FROM comentarios 
                            WHERE id = @UltimoComentario
                        )
                    )
                    ORDER BY c.created_at DESC
                    LIMIT 20
                ";


                IEnumerable<GetRegistroDeComentarioResponse> registros = await connection.QueryAsync<GetRegistroDeComentarioResponse, GetHiloRegistroResponse,GetHiloMiniaturaResponse, GetRegistroDeComentarioResponse>(sql,
                    (comentario, hilo, miniatura) =>
                    {
                        hilo.Miniatura = miniatura;
                        return new GetRegistroDeComentarioResponse
                        {
                            Id = comentario.Id,
                            Contenido = comentario.Contenido,
                            Fecha = comentario.Fecha,
                            Hilo = hilo
                        };
                    }, 
                new
                {
                    request.Usuario,
                    request.UltimoComentario
                },  
                splitOn : "id,provider"
                );

                return registros.ToList();
            }

        }
    }

}