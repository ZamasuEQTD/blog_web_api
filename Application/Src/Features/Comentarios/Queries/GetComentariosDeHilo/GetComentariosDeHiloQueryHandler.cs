using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Comentarios.Queries.GetComentariosDeHilo.Builder;
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
                        comentario.autor_id as autorid,
                        comentario.tag,
                        comentario.dados,
                        comentario.tag_unico as tagunico,
                        comentario.color,
                        comentario.recibir_notificaciones as recibirnotificaciones
                    FROM comentarios comentario
                    /**Order**/
                    /**Where**/
                ";

                ComentariosBuilder comentariosBuilder = new ComentariosBuilder();

                List<IRule<ComentariosBuilder>> rules = [
                    new FiltrarPorHiloRule(request.Hilo),
                    new OrdenarPorCreacionRule(),
                    new FiltrarPorStatusRule(ComentarioStatus.Activo),
                    new FiltrarPorCreacionRule(request.UltimoComentario),
                ];

                foreach (var rule in rules)
                {
                    if (rule.IsRuleApplicable(comentariosBuilder)) rule.ApplyRule(comentariosBuilder);
                }   

                
                List<GetComentarioQuery> destacados = [];

                if (request.UltimoComentario is null)
                {
                    ComentariosBuilder destacadosBuilder = new ComentariosBuilder();

                    List<IRule<ComentariosBuilder>> destacadosRules = [
                        new FiltrarPorHiloRule(request.Hilo),
                        new OrdenarPorCreacionRule(),
                        new SoloDestacadosRule()
                    ];

                    foreach (var rule in destacadosRules)
                    {
                        if (rule.IsRuleApplicable(destacadosBuilder)) rule.ApplyRule(destacadosBuilder);
                    }

                    SqlBuilder.Template destacados_template = destacadosBuilder.builder.AddTemplate(sql);

                    await connection.QueryAsync(destacados_template.RawSql);
                }
               
                SqlBuilder.Template template = comentariosBuilder.builder.AddTemplate(sql);

                IEnumerable<GetComentarioDBResponse> response = await connection.QueryAsync<GetComentarioDBResponse>(template.RawSql, template.Parameters);

                

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
    }
}