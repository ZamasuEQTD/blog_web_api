using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Comentarios.GetComentarioDeHilos;
using SharedKernel;
using Dapper;
using Application.Features.Hilos.Queries.GetHilo;
using Application.Abstractions;
namespace Application.Comentarios.GetComentariosDeHilo;

public class GetComentariosDeHiloQueryHandler : IQueryHandler<GetComentariosDeHiloQuery, List<GetComentarioResponse>>
{
    private readonly IDBConnectionFactory _connection;
    private readonly IUserContext _user;

    public GetComentariosDeHiloQueryHandler(IDBConnectionFactory connection, IUserContext user)
    {
        _connection = connection;
        _user = user;
    }

    public async Task<Result<List<GetComentarioResponse>>> Handle(GetComentariosDeHiloQuery request, CancellationToken cancellationToken)
    {
        var sql = @$"(
            SELECT 
                comentario.id,
                comentario.created_at as createdat,
                comentario.texto,
                comentario.autor_id as autorid,
                comentario.color,
                comentario.autor_id = @UsuarioId AS EsAutor,
                CASE
                    WHEN comentario.autor_id = @UsuarioId THEN comentario.recibir_notificaciones
                    ELSE NULL
                END AS RecibirNotificaciones,
                NULL as destacado_at,  
                FALSE as destacado,
                comentario.autor_nombre as nombre,
                comentario.rango_corto as rangocorto,
                comentario.rango,
                comentario.tag,
                comentario.dados,
                comentario.tag_unico as tagunico
            FROM comentarios comentario
            /**where**/
            {
            (request.UltimoComentario == DateTime.MinValue ?
            @$"
            UNION ALL
            SELECT 
                comentario.id,
                comentario.created_at as createdat,
                comentario.texto,
                comentario.autor_id as autorid,
                comentario.color,
                comentario.autor_id = @UsuarioId AS EsAutor,
                CASE
                    WHEN comentario.autor_id = @UsuarioId THEN comentario.recibir_notificaciones
                    ELSE NULL
                END AS RecibirNotificaciones,
                destacado.created_at as destacado_at,  
                true as destacado,
                comentario.autor_nombre as nombre,
                comentario.rango_corto as rangocorto,
                comentario.rango,
                comentario.tag,
                comentario.dados,
                comentario.tag_unico as tagunico
            FROM comentarios comentario
            JOIN comentarios_destacados destacado ON destacado.comentario_id = comentario.id
            /**where**/
            " : "")
            }
            )
            ORDER BY destacado_at DESC, createdat DESC
            LIMIT 20
        ";
        using var connection = _connection.CreateConnection();

        SqlBuilder builder = new SqlBuilder().Where("comentario.hilo_id = @HiloId", new {request.HiloId});

        DynamicParameters parameters = new DynamicParameters();

        parameters.AddDynamicParams(new {
            UsuarioId = _user.IsLogged ? (Guid?) _user.UsuarioId : null
        });

        if(request.UltimoComentario != DateTime.MinValue){
            builder.Where("comentario.created_at < @UltimoComentario", new {UltimoComentario = request.UltimoComentario});
        } 

        builder.AddParameters(parameters);

        var template = builder.AddTemplate(sql);

        IEnumerable<GetComentarioResponse> comentarios = await connection.QueryAsync<GetComentarioResponse, GetHiloAutorResponse, GetComentarioDetallesResponse, GetComentarioResponse>(
        template.RawSql, 
        (comentario, autor, detalles) => {
            comentario.Autor = autor;
            
            comentario.Detalles = detalles;
            
            return comentario;
        },
        param: template.Parameters, 
        splitOn: "nombre,tag"
        );

        return Result.Success(comentarios.ToList());
    }
}