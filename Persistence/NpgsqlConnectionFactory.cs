using System.Data;
using Application.Abstractions.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace Persistence.Configuration
{
    public class NpgsqlConnectionFactory : IDBConnectionFactory
    {
        private readonly string _connectionString;
        public NpgsqlConnectionFactory(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection")!;
        }

        public IDbConnection CreateConnection()
        {
            return new NpgsqlConnection(_connectionString);
        }
    }
}