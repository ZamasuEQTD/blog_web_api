using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Dapper;
using Domain.Hilos.ValueObjects;
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
        private readonly IDateTimeProvider _timeProvider;
        public GetPortadasHomeQueryHandler(IDBConnectionFactory connection, IUserContext user, IDateTimeProvider timeProvider)
        {
            _connection = connection;
            _user = user;
            _timeProvider = timeProvider;
        }

        public async Task<Result<List<GetPortadaHomeResponse>>> Handle(GetPortadasHomeQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {

                List<object> stickies = [];

                if (request.UltimoBump is null)
                {
                    string stickiesSql = @"
                    ";

                    await connection.QueryAsync(stickiesSql);
                }
                string sql = @"
                SELECT
                    hilo.id AS Id,
                    hilo.titulo AS Titulo,
                    hilo.descripcion AS Descripcion,
                    hilo.encuesta_id AS Encuesta,
                    hilo.autor_id AS Autor,
                    subcategoria.id AS Id,
                    subcategoria.nombre_corto AS Categoria
                    portada_reference.spoiler AS Spoiler,
                    portada.path,
                    portada.miniatura
                    portada.tipo_de_archivo
                FROM hilos hilo
                JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                JOIN media_references portada_reference ON portada_reference.id = hilo.portada_id
                JOIN medias portada ON portada.id = portada_reference.media_id
                /**where**/
                ORDER BY DESC hilo.ultimo_bump
                ";

                SqlBuilder builder = new SqlBuilder();

                if (request.Titulo is not null)
                {
                    builder = builder.Where("hilo.titulo ~ @titulo", new
                    {
                        titulo = request.Titulo
                    });
                }

                List<Guid> categorias_habilitadas;

                if (request.Categoria is Guid categoria)
                {
                    categorias_habilitadas = [categoria];
                }
                else
                {
                    categorias_habilitadas = (await connection.QueryAsync<Guid>(@"
                    SELECT
                        id
                    FROM subcategorias
                    ")).ToList();
                }

                builder = builder.Where("subcategoria.id = ANY (@categorias_habilitadas)", new
                {
                    categorias_habilitadas
                });

                if (_user.IsLogged)
                {
                    builder = builder.Where("NOT (hilo.id = ANY (@hilos_bloqueados))", new
                    {
                        hilos_bloqueados = (await connection.QueryAsync<Guid>("")).ToList()
                    });
                }

                if (request.UltimoBump != DateTime.MinValue)
                {
                    builder = builder.Where("hilo.ultimo_bump < @ultimo_bump ::date", new
                    {
                        ultimo_bump = (DateTime)request.UltimoBump!
                    });
                }

                builder = builder.Where("h.status == @status", new
                {
                    status = HiloStatus.Activo.Value
                });

                SqlBuilder.Template template = builder.AddTemplate(sql);

                return (await connection.QueryAsync<GetPortadaHomeResponse>(template.RawSql, template.Parameters)).ToList();

                List<GetPortadaHome> portadas = [];

                portadas.Select(p => new GetPortada()
                {
                    Id = p.Id,
                    Title = p.Title
                });
            }
        }
    }

    public class GetPortadaHome
    {
        public Guid Id { get; set; }
        public Guid Autor { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public bool Spoiler { get; set; }
        public string Image { get; set; }
    }

    public class GetPortada
    {
        public Guid Id { get; set; }
        public Guid? Autor { get; set; }
        public GetImagenPortadaRespose Imagen { get; set; }
        public bool Sticky { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
    }

    public class GetBanderasDePortadaResponse
    {
        public bool Dados { get; set; }
        public bool IdUnico { get; set; }
    }

    public class GetImagenPortadaRespose
    {
        public bool Spoiler { get; set; }
        public string Imagen { get; set; }
    }
}