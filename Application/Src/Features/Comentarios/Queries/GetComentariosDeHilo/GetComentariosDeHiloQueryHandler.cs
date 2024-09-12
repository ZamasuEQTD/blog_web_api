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
                bool hilo_activo = true;

                if (hilo_activo) return HilosFailures.Inactivo;

                string sql = @"
                    SELECT
                        comentario.id,
                        comentario.texto,
                        comentario.autor_id,
                        comentario.tag,
                        comentario.dados,
                        comentario.tag_unico,
                        comentario.color
                    FROM comentarios comentario
                    JOIN comentarios_destacados destacado ON destacado.comentario_id = comentario.id
                    ORDER BY comentario.created_at DESC
                ";

                SqlBuilder builder = new SqlBuilder();

                builder.Where("comentario.hilo_id = @hilo", new
                {
                    hilo = request.Hilo,
                });

                List<object> destacados = [];

                if (request.UltimoComentario is null)
                {
                    var destacados_builder = builder.Join("comentarios_destacados destacado ON destacado.comentario_id = comentario.id");

                    SqlBuilder.Template destacados_template = destacados_builder.AddTemplate("");

                    await connection.QueryAsync(destacados_template.RawSql);


                }

                if (request.UltimoComentario is not null)
                {
                    DateTime? created_at = await connection.QueryFirstAsync<DateTime?>("");

                    if (created_at is not null)
                    {
                        builder = builder.Where("comentario.created_at < @created_at ::Date", new
                        {
                            created_at
                        });
                    }
                }

                builder = builder.Where("comentario.status = @status", new
                {
                    status = ComentarioStatus.Activo.Value
                });

                SqlBuilder.Template template = builder.AddTemplate("");

                await connection.QueryAsync(template.RawSql, template.Parameters);
            }

            throw new NotImplementedException();
        }
    }

    public class GetComentario
    {
        public string Texto { get; set; }
    }
}