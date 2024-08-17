using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Comentarios
{
    public class GetComentariosDeHiloQueryHandler : IQueryHandler<GetComentariosDeHiloQuery, List<GetComentarioResponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetComentariosDeHiloQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetComentarioResponse>>> Handle(GetComentariosDeHiloQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                SqlBuilder builder = new SqlBuilder();

                builder.Where("comentario.hilo_id = @hilo", new
                {
                    hilo = request.Hilo,
                });

                if (request.UltimoComentario is not null)
                {

                    DateTime created_at = await connection.QueryFirstAsync<DateTime>("");
                    builder = builder.Where("comentario.created_at < @created_at ::Date", new
                    {
                        created_at
                    });
                }

                SqlBuilder.Template template = builder.AddTemplate("");

                await connection.QueryAsync("", template.Parameters);
            }

            throw new NotImplementedException();
        }
    }
}