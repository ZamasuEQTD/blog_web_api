using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Features.Medias.Services;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Hilos.Queries
{
    public interface IHomeContext
    {
        public List<Guid> CategoriasHabilitadas { get; }
    }
    public class GetPortadasHomeQueryHandler : IQueryHandler<GetPortadasHomeQuery, List<GetPortadaHomeResponse>>
    {
        private readonly IUserContext _user;
        private readonly IDBConnectionFactory _connection;
        private readonly IDateTimeProvider _time;
        public GetPortadasHomeQueryHandler(IDBConnectionFactory connection, IUserContext user, IDateTimeProvider timeProvider)
        {
            _connection = connection;
            _user = user;
            _time = timeProvider;
        }

        public async Task<Result<List<GetPortadaHomeResponse>>> Handle(GetPortadasHomeQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                string sql = @"
                    (
                    SELECT
                        hilo.id,
                        hilo.titulo,
                        hilo.created_at as createdat,
                        hilo.ultimo_bump as ultimobump,
                        hilo.usuario_id AS autor,
                        hilo.encuesta_id AS encuesta,
                        hilo.dados,
                        hilo.id_unico_activado AS idunico,
                        subcategoria.nombre_corto AS nombredecategoria,
                        subcategoria.id AS subcategoriaid,
                        portada_reference.spoiler,
                        portada.path,
                        portada.hash,
                        portada.tipo_de_archivo as tipodearchivo,
                        false as sticky
                    FROM hilos hilo
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                    JOIN media_references portada_reference ON portada_reference.id = hilo.portada_id
                    JOIN media portada ON portada.id = portada_reference.media_id
                    /**where**/
                    ORDER BY hilo.ultimo_bump DESC
                    LIMIT 20
                    )
                    ORDER BY sticky DESC, ultimobump DESC
                ";
 
                SqlBuilder portadas_builder = new SqlBuilder();

                if(!request.UltimoBump.HasValue) {
                    portadas_builder.Where($"hilo.ultimo_bump < @ultimo_bump", new { ultimo_bump = request.UltimoBump });
                }

                if(request.Titulo is not null){
                    portadas_builder.Where($"hilo.titulo ~ '{request.Titulo}'");
                }

                if(request.Categoria is not null ){
                    portadas_builder.Where($"hilo.subcategoria_id = '{request.Categoria}'");
                }

                if(_user.IsLogged){
                    portadas_builder.Where($@"
                    hilo.id NOT IN (
                        SELECT
                            hilo_id
                        FROM hilo_interacciones
                        WHERE 
                            usuario_id = '{_user.UsuarioId}'
                            AND
                            oculto 
                    )");
                }

                string? stickies_sql = null;

                if(request.UltimoBump == DateTime.MinValue){
                    stickies_sql = $@"
                    SELECT
                        hilo.id,
                        hilo.titulo,
                        hilo.created_at as createdat,
                        hilo.ultimo_bump as ultimobump,
                        hilo.usuario_id AS autor,
                        hilo.encuesta_id AS encuesta,
                        hilo.dados,
                        hilo.id_unico_activado AS idunico,
                        subcategoria.nombre_corto AS nombredecategoria,
                        subcategoria.id AS subcategoriaid,
                        portada_reference.spoiler,
                        portada.path,
                        portada.hash,
                        portada.tipo_de_archivo as tipodearchivo,
                        true as sticky
                    FROM hilos hilo
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                    JOIN media_references portada_reference ON portada_reference.id = hilo.portada_id
                    JOIN media portada ON portada.id = portada_reference.media_id
                    JOIN stickies sticky ON hilo.id = sticky.hilo_id
                ";
                }
                SqlBuilder.Template template = portadas_builder.AddTemplate(sql);


                string raw_sql = (stickies_sql is not null? $"{stickies_sql} \nUNION\n" : "")
                        +
                    template.RawSql;
                
                
                IEnumerable<PortadaResponse> response =  await connection.QueryAsync<PortadaResponse>(
                raw_sql,
                template.Parameters
                );
                
                List<GetPortadaHomeResponse> portadas = [];
    
                foreach (var portada in response)
                {
                    string miniatura;

                    if (portada.TipoDeArchivo == "youtube")
                    {
                        miniatura = YoutubeService.GetVideoThumbnailFromUrl(portada.Path);
                    }
                    else
                    {
                        miniatura = "/media/thumbnails/" + portada.Hash + ".jpeg";
                    }

                    portadas.Add(new GetPortadaHomeResponse()
                    {
                        Id = portada.Id,
                        Titulo = portada.Titulo,
                        AutorId = _user.IsLogged && _user.Rango ==  Domain.Usuarios.RangoDeUsuario.Moderador? portada.Autor : null,
                        EsNuevo = _time.UtcNow.Subtract(portada.CreatedAt).TotalMinutes < 20,
                        Spoiler = portada.Spoiler,
                        Miniatura = miniatura,
                        UltimoBump = portada.UltimoBump,
                        Autor = new GetAutor()
                        {
                            Nombre = portada.AutorNombre,
                            RangoCorto = portada.RangoCorto,
                            Rango = portada.Rango
                        },
                        EsOp = _user.IsLogged && _user.UsuarioId == portada.Autor,
                        EsSticky = portada.Sticky,
                        Subcategoria = new GetSubcategoria()
                        {
                            Id = portada.SubcategoriaId,
                            Nombre = portada.NombreDeCategoria
                        },
                        Banderas = new GetBanderas()
                        {
                            Dados = portada.Dados,
                            Encuesta = portada.Encuesta is not null,
                            IdUnico = portada.IdUnico
                        },
                    });
                }

                return portadas;
            }
        }
    }

}