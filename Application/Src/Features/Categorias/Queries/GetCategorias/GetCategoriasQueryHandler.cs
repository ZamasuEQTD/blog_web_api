using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Categorias.Queries
{
    public class GetCategoriasQueryHandler : IQueryHandler<GetCategoriasQuery, List<GetCategoriaReponse>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetCategoriasQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetCategoriaReponse>>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
        {
            Dictionary<Guid, GetCategoriaReponse> categorias = [];

            string sql = @"
            SELECT
                c.nombre AS Nombre,
                s.id AS Id,
                s.id as Id,
                s.nombre AS Nombre
            FROM categorias c
            LEFT JOIN subcategorias s ON s.categoria_id = c.id
            ";

            using (var connection = _connection.CreateConnection())
            {
                await connection.QueryAsync<GetCategoriaReponse, GetSubcategoriaResponse, GetCategoriaReponse>(sql, (categoria, subcategoria) =>
                {
                    if (categorias.TryGetValue(categoria.Id, out var c))
                    {
                        categoria = c;
                    }
                    else
                    {
                        categorias.Add(categoria.Id, categoria);
                    }

                    categoria.Subcategorias.Add(subcategoria);

                    return categoria;
                }, splitOn: "Id");
            }

            List<GetCategoriaReponse> response = categorias.Select(c => c.Value).OrderByDescending(c => c.OcultaDesdePrincipio).ToList();

            return response;
        }
    }
}