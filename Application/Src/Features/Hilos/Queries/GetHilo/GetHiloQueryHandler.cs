using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Hilos.Queries
{
    public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
    {
        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _context;
        public GetHiloQueryHandler(IUserContext context, IDBConnectionFactory connection)
        {
            _context = context;
            _connection = connection;
        }

        public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                IEnumerable<GetHiloResponse> hilo = await connection.QueryAsync<GetHiloResponse>($@"
                    SELECT
                        hilo.id AS Id,
                        COUNT(1) FILTER (WHERE c.hilo_id = h.id) as Comentarios,
                        hilo.titulo AS Titulo,
                        hilo.descripcion AS Descripcion,
                        hilo.autor_id AS AutorId,
                        hilo.created_at AS CreatedAt,
                        subcategoria.id AS Id,
                        subcategoria.nombre_corto AS Categoria,
                    FROM hilos hilo
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                    "
                );

                GetHiloResponse response = hilo.First();

                response.EsOp = _context.IsLogged && response.Id == response.AutorId;

                if (response.EncuestaId is not null)
                {
                    var encuesta = await connection.QueryAsync<GetEncuestaResponse>("...");

                    GetEncuestaResponse encuesta_response = encuesta.First();

                    encuesta_response.OpcionVotada = _context.IsLogged ? (await connection.QueryAsync<Guid>(@"
                        SELECT
                            respuesta.id
                        FROM votos voto
                        JOIN respuestas respuesta ON voto.encuesta_id = respuesta.encuesta_id
                        WHERE voto.votante_id = @suarioId
                    ", new
                    {
                        _context.UsuarioId
                    })).FirstOrDefault() : null;

                    response.Encuesta = encuesta.First();
                }

                return response;
            }
        }
    }

    public class HiloQuery
    {

    }
}