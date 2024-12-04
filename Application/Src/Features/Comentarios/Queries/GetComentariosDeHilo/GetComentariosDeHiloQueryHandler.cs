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
                        comentario.autor_nombre as nombre,
                        comentario.rango_corto as rangocorto,
                        comentario.rango,
                        comentario.recibir_notificaciones as recibirnotificaciones,
                        NULL as destacado_at,  
                        FALSE as destacado
                    FROM comentarios comentario
                    JOIN usuarios autor ON comentario.autor_id = autor.id
                    /**where**/
                    ORDER BY destacado DESC, destacado_at DESC, createdat DESC 
                ";

                SqlBuilder comentarios_builder = new SqlBuilder()
                .Where($"comentario.hilo_id = '{request.Hilo}'");

                if(_user.IsLogged){
                    comentarios_builder.Where(
                        $@"comentario.id NOT IN (
                            SELECT 
                                id
                            FROM comentario_interacciones  
                            WHERE  usuario_id = '{_user.UsuarioId}' AND   oculto
                        )"
                    );
                }

                string? destacados_sql = null;

                if(!request.UltimoComentario.HasValue){
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
                        comentario.autor_nombre as nombre,
                        comentario.rango_corto as rangocorto,
                        comentario.rango,
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
                    comentarios_builder.Where($"comentario.created_at < @ultimo_comentario", new { ultimo_comentario = request.UltimoComentario });
                }

                SqlBuilder.Template template = comentarios_builder.AddTemplate(sql);

                IEnumerable<GetComentarioDBResponse> response = await connection.QueryAsync<GetComentarioDBResponse>(
                    (destacados_sql is not null? destacados_sql + "\nUNION\n": "")
                    + 
                    template.RawSql,
                    template.Parameters
                );

                List<GetComentarioResponse> comentarios = response.Select((c)=> new GetComentarioResponse(){
                    Id = c.Id,
                    CreatedAt = c.CreatedAt,
                    Texto = c.Texto,
                    AutorId = _user.IsLogged?  c.AutorId:null  ,
                    Tag = c.Tag,
                    TagUnico = c.TagUnico,
                    Dados = c.Dados,
                    Destacado = c.Destacado,
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