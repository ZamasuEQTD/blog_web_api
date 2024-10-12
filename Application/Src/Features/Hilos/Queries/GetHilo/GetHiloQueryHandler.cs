using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Categorias.Queries;
using Dapper;
using Domain.Comentarios;
using SharedKernel;

namespace Application.Hilos.Queries
{
    public class GetHiloQueryHandler : IQueryHandler<GetHiloQuery, GetHiloResponse>
    {
        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _context;
        public GetHiloQueryHandler(IUserContext context, IDBConnectionFactory connection)
        {
            _context = context;
            _connection = connection;
        }

        public async Task<Result<GetHiloResponse>> Handle(GetHiloQuery request, CancellationToken cancellationToken)
        {
            using (var connection = _connection.CreateConnection())
            {
                GetHiloResponse? hilo = null;

                using (var query = await connection.QueryMultipleAsync(@"
                    SELECT
                        hilo.id,
                        hilo.titulo,
                        hilo.descripcion,
                        hilo.usuario_id as autorid,
                        hilo.created_at as createdat,
                        (
	                    SELECT
	                    	count(c.id)
	                    FROM comentarios c
	                    WHERE c.hilo_id = hilo.id
	                    ) AS comentarios,
                        subcategoria.id AS subcategoriaid,
                        subcategoria.nombre_corto AS nombresubcategoria,
                        portada_reference.spoiler,
                        portada.path,
                        portada.hash,
                        portada.tipo_de_archivo as tipodearchivo
                    FROM hilos hilo
                    JOIN subcategorias subcategoria ON subcategoria.id = hilo.subcategoria_id
                    JOIN media_references portada_reference ON portada_reference.id = hilo.portada_id
                    JOIN media portada ON portada.id = portada_reference.media_id
                    WHERE hilo.id = @hilo;
                    SELECT
                        r.id,
                        r.encuesta_id as encuestaid,
                        r.contenido,
                        (
                            SELECT
                                COUNT(v.id)
                            FROM votos v
                            WHERE r.id = v.id
                        ) as  votos
                    FROM respuestas r
                    LEFT JOIN hilos h ON h.encuesta_id = r.encuesta_id
                    WHERE h.id = @hilo;
                ", new
                {
                    hilo = request.Hilo
                }))
                {
                    var hilo_query = query.Read<HiloResponse>();

                    var encuesta_query = query.Read<RespuestaResponse>();

                    var hilo_response = hilo_query.FirstOrDefault();

                    if (hilo_response is null) return HilosFailures.NoEncontrado;

                    GetEncuestaResponse? encuesta = null;

                    if (encuesta_query.Any())
                    {
                        encuesta = new GetEncuestaResponse()
                        {
                            Id = encuesta_query.First().EncuestaId,
                            Opciones = encuesta_query.Select(x => new GetOpcionResponse()
                            {
                                Id = x.Id,
                                Nombre = x.Contenido,
                                Votos = x.Votos
                            }).ToList()
                        };
                    }

                    hilo = new GetHiloResponse()
                    {
                        Id = hilo_response.Id,
                        Titulo = hilo_response.Titulo,
                        Descripcion = hilo_response.Descripcion,
                        EsOp = _context.IsLogged && hilo_response.AutorId == _context.UsuarioId,
                        CreatedAt = hilo_response.CreatedAt,
                        Encuesta = encuesta,
                        Subcategoria = new GetSubcategoriaResponse()
                        {
                            Id = hilo_response.SubcategoriaId,
                            Nombre = hilo_response.NombreDeCategoria
                        },
                        Media = new GetMediaResponse()
                        {
                            Tipo = hilo_response.TipoDeArchivo,
                            Previsualizacion = hilo_response.TipoDeArchivo == "video" ? $"/media/previsualizaciones/{hilo_response.Hash}.png" : null,
                            Url = hilo_response.TipoDeArchivo != "youtube" ? $"/media/files/{Path.GetFileName(hilo_response.Path)}" : hilo_response.Path
                        }
                    };

                    return hilo;
                }
            }
        }
    }

    public class HiloQuery
    {

    }
}