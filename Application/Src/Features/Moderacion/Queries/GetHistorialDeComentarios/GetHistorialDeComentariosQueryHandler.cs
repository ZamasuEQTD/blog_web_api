using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Moderacion
{
    public class GetHistorialDeComentariosQueryHandler : IQueryHandler<GetHistorialDeComentariosQuery, List<GetHistorialDeComentarioResponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetHistorialDeComentariosQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetHistorialDeComentarioResponse>>> Handle(GetHistorialDeComentariosQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                var sql = @"
                    SELECT
                        c.id,
                        c.texto,
                        h.id,
                        h.titulo,
                    FROM comentarios c
                    WHERE c.autor_id = @Usuario
                    WHERE c.estado = 'Activo'
                    JOIN hilos h ON h.id = c.hilo_id
                    JOIN medias portada ON p.id h = h.portada_id
                    ORDER BY c.created_at DESC
                    LIMIT 20 OFFSET @Offset
                ";

                await connection.QueryAsync(sql, new
                {
                    Offset = request.Pagina * 20
                });

                var res = new List<GetHistorialDeComentarioResponse>();

                return res;
            }

        }
    }

}