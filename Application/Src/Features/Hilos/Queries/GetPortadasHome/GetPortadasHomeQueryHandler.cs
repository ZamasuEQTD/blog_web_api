using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Hilos.Queries
{
    public interface IHomeContext
    {
        public List<Guid> CategoriasHabilitadas { get; }
    }
    public class GetPortadasHomeQueryHandler : IQueryHandler<GetPortadasHomeQuery, List<GetPortadaHomeResponse>>
    {
        private readonly IUserContext _user;
        private readonly IDBConnectionFactory _connection;
        public GetPortadasHomeQueryHandler(IDBConnectionFactory connection, IUserContext user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<List<GetPortadaHomeResponse>>> Handle(GetPortadasHomeQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                string sql = @"
                SELECT
                        hilo.id AS Id,
                        hilo.titulo AS Titulo,
                        hilo.descripcion AS Descripcion,
                        subcategoria.id AS Id,
                        subcategoria.nombre_corto AS Categoria
                    FROM hilos hilo
                        JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                    /**where**/
                ";

                SqlBuilder builder = new SqlBuilder();

                if (request.Titulo is not null)
                {
                    builder = builder.Where("hilo.titulo ~ @titulo", new
                    {
                        titulo = request.Titulo
                    });
                }

                List<Guid> categorias_habilitadas;

                if (request.Categoria is Guid categoria)
                {
                    categorias_habilitadas = [categoria];
                }
                else
                {
                    categorias_habilitadas = (await connection.QueryAsync<Guid>(@"
                    SELECT
                        id
                    FROM subcategorias
                    ")).ToList();
                }

                builder = builder.Where("subcategoria.id = ANY (@categorias_habilitadas)", new
                {
                    categorias_habilitadas
                });

                if (_user.IsLogged)
                {
                    builder = builder.Where("NOT (hilo.id = ANY (@hilos_bloqueados))", new
                    {
                        hilos_bloqueados = (await connection.QueryAsync<Guid>("")).ToList()
                    });
                }

                if (request.UltimoBump != DateTime.MinValue)
                {
                    builder = builder.Where("hilo.ultimo_bump < @ultimo_bump ::date", new
                    {
                        ultimo_bump = (DateTime)request.UltimoBump!
                    });
                }

                SqlBuilder.Template template = builder.AddTemplate(sql);

                return (await connection.QueryAsync<GetPortadaHomeResponse>(template.RawSql, template.Parameters)).ToList();
            }
        }
    }
}