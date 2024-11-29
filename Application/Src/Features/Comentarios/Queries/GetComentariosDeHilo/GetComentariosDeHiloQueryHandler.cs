using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using SharedKernel;

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
                        comentario.created_at as createdat,
                        comentario.texto,
                        comentario.autor_id as autorid,
                        comentario.tag,
                        comentario.dados,
                        comentario.tag_unico as tagunico,
                        comentario.color,
                        comentario.recibir_notificaciones as recibirnotificaciones,
                        NULL as destacado_at,  
                        FALSE as destacado,
                        autor.nombre,
                        autor.rango,
                        autor.rango_corto as rangocorto
                    FROM comentarios comentario
                    JOIN usuarios autor ON comentario.autor_id = autor.id
                    /**Where**/
                    ORDER BY createdat DESC, destacado_at DESC
                ";

                SqlBuilder comentarios_builder = new SqlBuilder()
                .Where($"comentario.hilo_id = '{request.Hilo}'");

                if(_user.IsLogged){
                    comentarios_builder.Where(
                        $@"comentario.id NOT IN (
                            SELECT 
                                interaccion.id
                            FROM interacion_comentario
                            WHERE interacion.usuario_id = '{_user.UsuarioId}' AND interaccion.oculto
                        )"
                    );
                }

                string? destacados_sql = null;

                if(request.UltimoComentario is null){
                    destacados_sql = $@"
                    SELECT
                        comentario.id,
                        comentario.created_at as createdat,
                        comentario.texto,
                        comentario.autor_id as autorid,
                        comentario.tag,
                        comentario.dados,
                        comentario.tag_unico as tagunico,
                        comentario.color,
                        comentario.recibir_notificaciones as recibirnotificaciones,
                        destacado.created_at as destacado_at,
                        TRUE as destacado
                    FROM comentarios comentario
                    JOIN comentarios_destacados destacado ON comentario.id = destacado.comentario_id
                    JOIN usuarios autor ON comentario.autor_id = autor.id
                    WHERE comentario.hilo_id = '{request.Hilo}'
                    ";
                } 
                else {
                    comentarios_builder.Where($"comentario.created_at < {request.UltimoComentario}");
                }

                IEnumerable<GetComentarioDBResponse> response = await connection.QueryAsync<GetComentarioDBResponse>(
                    (destacados_sql is not null? destacados_sql + "\nUNION\n": "")
                    + 
                    sql 
                );

                List<GetComentarioResponse> comentarios = response.Select((c)=> new GetComentarioResponse(){
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    Texto = c.Texto,
                    AutorId = _user.IsLogged?  c.AutorId:null  ,
                    Tag = c.Tag,
                    TagUnico = c.TagUnico,
                    Dados = c.Dados,
                    Color = c.Color,
                    RecibirNotificaciones = c.RecibirNotificaciones,
                    Autor =   new GetComentarioAutorResponse(){
                        Nombre = c.Nombre,
                        Rango = c.Rango,
                        RangoCorto = c.RangoCorto
                    }  
                }).ToList();

                return comentarios;
            }

        }
    }
 

    public class GetComentarioDBResponse
    {
        public Guid Id { get; set; }
        public Guid AutorId { get; set; }
        public DateTime CreatedAt { get; set; }
        public string Texto { get; set; }
        public bool Destacado { get; set; }
        public string Tag { get; set; }
        public string? TagUnico { get; set; }
        public string Color { get; set; }
        public string? Dados { get; set; }
        public bool RecibirNotificaciones { get; set; }
        public string Nombre { get; set; }
        public string Rango { get; set; }
        public string RangoCorto { get; set; }
    }
}