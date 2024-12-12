using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos.Queries.Responses;
using Dapper;
using SharedKernel;

namespace Application.Features.Hilos.Queries.GetColeccion;

public class GetColeccionQueryHandler : IQueryHandler<GetColeccionQuery, IEnumerable<GetHiloPortadaResponse>>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;

    public GetColeccionQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<IEnumerable<GetHiloPortadaResponse>>> Handle(GetColeccionQuery request, CancellationToken cancellationToken)
    {
        using var connection = _connection.CreateConnection();

        var query = "SELECT * FROM Hilos";

        SqlBuilder builder = new();

        builder.Where("hilo.status = 'Activo' AND interacion.usuario_id = @UsuarioId", new {  _user.UsuarioId });

        if (request.UltimoHilo.HasValue)
        {
            builder.Where("@UltimoHilo IS NULL OR hilo.creadted < (SELECT created_at FROM Hilos WHERE id = @UltimoHilo)", new { UltimoHilo = request.UltimoHilo.Value });
        }

        if (request.Tipo == TipoDeColeccion.Favoritos)
        {
            builder.Where("interaccion.favorito = true");
        } else if (request.Tipo == TipoDeColeccion.Ocultos)
        {
            builder.Where("interaccion.oculto = true");
        } else if(request.Tipo == TipoDeColeccion.Seguidos)
        {
            builder.Where("interaccion.seguido = true");
        }

        SqlBuilder.Template template = builder.AddTemplate(query);

        var hilos = await connection.QueryAsync<GetHiloPortadaResponse>(template.RawSql, template.Parameters);

        return Result.Success(hilos);
    }
}