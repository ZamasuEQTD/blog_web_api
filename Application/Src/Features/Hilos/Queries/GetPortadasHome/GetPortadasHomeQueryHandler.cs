using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;
using Dapper;
using Domain.Categorias;
using Domain.Comentarios.Services;
using Domain.Hilos.ValueObjects;
using Domain.Media.Services;
using SharedKernel;
using SharedKernel.Abstractions;
using static Dapper.SqlBuilder;

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
                string sql = @"
                SELECT
                    hilo.id,
                    hilo.titulo,
                    hilo.usuario_id AS autor,
                    hilo.encuesta_id AS encuesta,
                    hilo.dados,
                    hilo.id_unico_activado AS idunico,
                    subcategoria.nombre_corto AS nombredecategoria,
                    subcategoria.id AS subcategoriaid,
                    portada_reference.spoiler,
                    portada.path,
                    portada.hash,
                    portada.tipo_de_archivo as tipodearchivo
                FROM hilos hilo
                JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                JOIN media_references portada_reference ON portada_reference.id = hilo.portada_id
                JOIN media portada ON portada.id = portada_reference.media_id
                /**where**/
 			    ORDER BY hilo.ultimo_bump DESC;
                ";

                FiltrosDePortadas filtros = new FiltrosDePortadas()
                {
                    Categoria = request.Categoria,
                    Titulo = request.Titulo,
                    UltimoBump = request.UltimoBump
                };

                HilosSQLBuilder portadas_builder = new HilosSQLBuilder(filtros);

                List<IPortadaRule> rules = [
                    new FiltrarPorTituloRule(),
                    new FiltrarPorCategoriaRule(),
                    new FiltrarPorCategoriasActivasRule(),
                    new FiltrarPortadasOcultasDeUsuarioRule(_user),
                    new FiltrarPorUltimoBumpRule()
                ];

                foreach (var rule in rules)
                {
                    if (rule.Matches(portadas_builder))
                    {
                        rule.Apply(portadas_builder);
                    }
                }

                Template portadas_template = portadas_builder.AddTemplate(sql);

                var por = await connection.QueryAsync<PortadaResponse>(portadas_template.RawSql, portadas_template.Parameters);

                List<GetPortadaHomeResponse> portadas = [];

                foreach (var portada in por)
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
                        Autor = _user.IsLogged && _user.Rango == Domain.Usuarios.Usuario.RangoDeUsuario.Moderador ? portada.Autor : null,
                        EsNuevo = (_timeProvider.UtcNow - portada.CreatedAt).Minutes < 20,
                        Spoiler = portada.Spoiler,
                        Miniatura = miniatura,
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


    public class FiltrosDePortadas
    {
        public Guid? Categoria { get; set; }
        public string? Titulo { get; set; }
        public DateTime? UltimoBump { get; set; }
    }

    public interface IPortadaRule : IRule<HilosSQLBuilder> { }

    public class FiltrarPorTituloRule : IPortadaRule
    {
        public void Apply(HilosSQLBuilder input)
        {
            input.PorTitulo(input.Filtros.Titulo!);
        }

        public bool Matches(HilosSQLBuilder input) => input.Filtros.Titulo is not null;
    }

    public class FiltrarPorCategoriaRule : IPortadaRule
    {
        public void Apply(HilosSQLBuilder input) => input.PorCategoria((Guid)input.Filtros.Categoria!);

        public bool Matches(HilosSQLBuilder input) => input.Filtros.Categoria.HasValue;
    }

    public class FiltrarPorCategoriasActivasRule : IPortadaRule
    {
        public void Apply(HilosSQLBuilder input)
        {
            input.PorCategoriasActivas();
        }

        public bool Matches(HilosSQLBuilder input) => input.Filtros.Categoria.HasValue;
    }
    public class FiltrarPortadasOcultasDeUsuarioRule : IPortadaRule
    {

        private readonly IUserContext _context;

        public FiltrarPortadasOcultasDeUsuarioRule(IUserContext context)
        {
            _context = context;
        }

        public void Apply(HilosSQLBuilder input)
        {
            input.RemoverOcultosPorUsuario(_context.UsuarioId);
        }

        public bool Matches(HilosSQLBuilder input) => _context.IsLogged;
    }

    public class FiltrarPorUltimoBumpRule : IPortadaRule
    {
        public void Apply(HilosSQLBuilder input)
        {
            input.PorUltimoBump((DateTime)input.Filtros.UltimoBump!);
        }

        public bool Matches(HilosSQLBuilder input) => input.Filtros.UltimoBump != DateTime.MinValue;
    }

    public class HilosSQLBuilder : SqlBuilder
    {
        public FiltrosDePortadas Filtros { get; private set; }

        public HilosSQLBuilder(FiltrosDePortadas filtros)
        {
            Filtros = filtros;
        }

        public HilosSQLBuilder PorTitulo(string titulo)
        {
            Where("hilo.titulo ~ @titulo", new
            {
                titulo
            });

            return this;
        }

        public HilosSQLBuilder PorCategoria(Guid categoria)
        {

            Where("subcategoria.id =  @categoria", new
            {
                categoria
            });

            return this;
        }

        public HilosSQLBuilder PorCategoriasActivas()
        {
            Where(@"subcategoria.id IN (
                SELECT
                    id
                FROM subcategorias
            )");

            return this;
        }

        public HilosSQLBuilder PorUltimoBump(DateTime bump)
        {
            Where("hilo.ultimo_bump < @bump ::date", new
            {
                bump
            });
            return this;
        }

        public HilosSQLBuilder RemoverOcultosPorUsuario(Guid usuario)
        {
            Where(@"NOT hilo.id IN (
                        SELECT
                            hilo_id
                        FROM relaciones_de_hilo  
                        WHERE  oculto AND  usuario_id = @usuario
                    )", new
            {
                usuario
            });

            return this;
        }
        public HilosSQLBuilder SoloStickies()
        {
            Where("EXISTS (SELECT 1 FROM stickies s WHERE hilo.id = s.hilo_id)");

            return this;
        }
    }
}