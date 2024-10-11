using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Comentarios
{
    public class GetComentariosDeHiloQueryHandler : IQueryHandler<GetComentariosDeHiloQuery, List<GetComentarioResponse>>
    {
        private readonly IUserContext _user;
        private readonly IDBConnectionFactory _connection;

        public GetComentariosDeHiloQueryHandler(IDBConnectionFactory connection, IUserContext user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task<Result<List<GetComentarioResponse>>> Handle(GetComentariosDeHiloQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                string sql = @"
                    SELECT
                        comentario.id,
                        comentario.texto,
                        comentario.autor_id,
                        comentario.tag,
                        comentario.dados,
                        comentario.tag_unico,
                        comentario.color,
                        comentario.recibir_notificaciones
                    FROM comentarios comentario
                    ORDER BY c.createdat DESC
                ";

                SqlBuilder builder = new SqlBuilder();

                builder.Where("comentario.hilo_id = @hilo", new
                {
                    hilo = request.Hilo,
                });

                List<GetComentarioQuery> destacados = [];

                if (request.UltimoComentario is null)
                {
                    var destacados_builder = builder.Where("EXISTS (SELECT 1 FROM comentarios_destacados destacado WHERE destacado.id = comentario.id)");

                    SqlBuilder.Template destacados_template = destacados_builder.AddTemplate("");

                    await connection.QueryAsync(destacados_template.RawSql);
                }
                else
                {
                    builder = builder.Where("comentario.created_at < @created_at ::Date", new
                    {
                        created_at = request.UltimoComentario
                    });
                }

                builder = builder.Where("comentario.status = @status", new
                {
                    status = ComentarioStatus.Activo.Value
                });

                SqlBuilder.Template template = builder.AddTemplate("");

                await connection.QueryAsync(template.RawSql, template.Parameters);

                List<GetComentarioQuery> response = [];

                List<GetComentarioResponse> comentarios = [
                .. destacados.Select(x=> new GetComentarioResponse(){
                    Id = x.Id,
                    AutorId = _user.IsLogged && _user.Rango == Domain.Usuarios.Usuario.RangoDeUsuario.Moderador? x.AutorId : null,
                    CreatedAt = x.CreatedAt,
                    Dados = x.Dados,
                    Destacado = true,
                    EsAutor = _user.IsLogged && _user.UsuarioId == x.AutorId,
                    RecibirNotificaciones  = _user.IsLogged && _user.UsuarioId == x.AutorId? x.RecibirNotificaciones : null,
                    Tag = x.Tag,
                    TagUnico = x.TagUnico,
                    Texto = x.Texto
                }),
                .. response.Select(x=>new GetComentarioResponse{
                    Id = x.Id,
                    AutorId = _user.IsLogged && _user.Rango == Domain.Usuarios.Usuario.RangoDeUsuario.Moderador? x.AutorId : null,
                    CreatedAt = x.CreatedAt,
                    Dados = x.Dados,
                    Destacado = false,
                    EsAutor = _user.IsLogged && _user.UsuarioId == x.AutorId,
                    RecibirNotificaciones  = _user.IsLogged && _user.UsuarioId == x.AutorId? x.RecibirNotificaciones : null,
                    Tag = x.Tag,
                    TagUnico = x.TagUnico,
                    Texto = x.Texto
                })
                ];

                return comentarios;
            }

        }
    }

    public class GetComentario
    {
        public string Texto { get; set; }
    }
}