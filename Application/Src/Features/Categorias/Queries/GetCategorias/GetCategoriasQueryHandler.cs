using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

namespace Application.Categorias.Queries
{
    public class GetCategoriasQueryHandler : IQueryHandler<GetCategoriasQuery, List<GetCategoriaDto>>
    {
        private readonly IDBConnectionFactory _connection;

        public GetCategoriasQueryHandler(IDBConnectionFactory connection)
        {
            _connection = connection;
        }

        public async Task<Result<List<GetCategoriaDto>>> Handle(GetCategoriasQuery request, CancellationToken cancellationToken)
        {
            var connection = _connection.CreateConnection();

            Dictionary<string, GetCategoriaDto> categoria = [];

            var categorias = await connection.QueryAsync("");

            foreach (var c in categorias)
            {

            }

            connection.Close();
            throw new NotImplementedException();
        }
    }
}