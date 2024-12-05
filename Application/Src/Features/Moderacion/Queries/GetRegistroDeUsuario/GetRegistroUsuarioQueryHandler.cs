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
            usuario.registrado_en as registradoen,
            baneo.concluye_en as concluye,
            moderador.username as moderador,
            baneo.mensaje
        FROM usuarios usuario 
        LEFT JOIN baneos baneo ON baneo.usuario_baneado_id = usuario.id
        LEFT JOIN usuarios moderador ON moderador.id = baneo.moderador_id
        WHERE usuario.id = @Usuario AND (baneo IS NULL OR baneo.concluye_en < @Now)
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

        IEnumerable<GetRegistroUsuarioResponse> response = await connection.QueryAsync<GetRegistroUsuarioResponse, GetBaneoResponse, GetRegistroUsuarioResponse>(_query,
        (usuario, baneo) => {
            usuario.UltimoBaneo = baneo;

            return usuario;
        },
        new {
            request.Usuario,
            Now = _time.UtcNow
        },
        splitOn : "id"
        );

        return Result.Success(response.First());
    }
}