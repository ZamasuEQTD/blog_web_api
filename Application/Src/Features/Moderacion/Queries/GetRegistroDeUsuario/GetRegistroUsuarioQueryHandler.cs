using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Baneos.Queries;
using Application.Moderacion;
using Dapper;
using MediatR;
using SharedKernel;
using SharedKernel.Abstractions;

class GetRegistroUsuarioQueryHandler : IQueryHandler<GetRegistroUsuarioQuery, GetRegistroUsuarioResponse>
{

    static readonly string _query = $@"
        SELECT
            usuario.id,
            usuario.username as nombre,
            usuario.registrado_en as registradoen
        FROM usuarios usuario 
        WHERE usuario.id = @Usuario
        ";

    private readonly IDBConnectionFactory _connection;
    private readonly IDateTimeProvider _time;
    public GetRegistroUsuarioQueryHandler(IDBConnectionFactory connection, IDateTimeProvider time)
    {
        _connection = connection;
        _time = time;
    }

    public async Task<Result<GetRegistroUsuarioResponse>> Handle(GetRegistroUsuarioQuery request, CancellationToken cancellationToken)
    {
        using var connection =   _connection.CreateConnection();

        IEnumerable<GetRegistroUsuarioResponse> response = await connection.QueryAsync<GetRegistroUsuarioResponse>(_query,
        new {
            request.Usuario,
        } 
        );

        return Result.Success(response.First());
    }
}