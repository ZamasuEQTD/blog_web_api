using System.Data;
using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Usuarios;
using MediatR;
using SharedKernel;

namespace Application.Baneos.Queries
{
    public class GetBaneoActivoQueryHandler : IQueryHandler<GetBaneoActivoQuery, BaneoDeUsuarioDto?>
    {
        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _user;
        public GetBaneoActivoQueryHandler(IDBConnectionFactory connection, IUserContext user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<BaneoDeUsuarioDto?>> Handle(GetBaneoActivoQuery request, CancellationToken cancellationToken)
        {
            IDbConnection connection = _connection.CreateConnection();

            string sql = "";

            var baneo = await connection.QuerySingleAsync<BaneoDeUsuarioDto>(sql, new
            {
                _user.UsuarioId
            });

            connection.Close();

            return baneo;
        }
    }
}