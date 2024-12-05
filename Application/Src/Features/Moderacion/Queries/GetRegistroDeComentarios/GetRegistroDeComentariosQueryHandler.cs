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
                        portada.hash
                    FROM comentarios c
                    JOIN hilos h ON h.id = c.hilo_id
                    JOIN medias portada ON p.id h = h.portada_id
                    WHERE c.status = 'Activo' AND c.created_at > @UltimoComentario AND c.autor_id = @Usuario
                    ORDER BY c.created_at DESC
                    LIMIT 20
                ";


                IEnumerable<GetRegistroDeComentarioResponse> registros = await connection.QueryAsync<GetRegistroDeComentarioResponse, GetHiloRegistroResponse, GetRegistroDeComentarioResponse>(sql,
                    (comentario, hilo) =>
                    {
                        hilo.Imagen = "/media/thumbnails/" + hilo.Hash + ".jpeg";

                        return new GetRegistroDeComentarioResponse
                        {
                            Comentario = comentario.Comentario,
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
                splitOn : "id"
                );

                return registros.ToList();
            }

        }
    }

}