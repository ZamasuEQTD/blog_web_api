using Application.Hilos.Commands;
using Application.Hilos.Queries;
using Infraestructure.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;
using Microsoft.AspNetCore.Authorization;
using Application.Hilos.Commands.PonerHiloEnFavorito;
using Application.Features.Hilos.Queries.GetHiloPortada;
using Application.Hilos.Queries.Responses;
using Application.Features.Hilos.Queries.GetHilo;
using Application.Features.Hilos.Queries.GetColeccion;
namespace WebApi.Controllers
{

    [Route("api/hilos")]
    public class HilosController : ApiController
    {
        public HilosController(ISender sender) : base(sender) { }

        [Authorize]
        [HttpPost("postear")]
        public async Task<IResult> Postear([FromForm] PostearHiloRequest request)
        {
            var result = await sender.Send(new PostearHiloCommand()
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Spoiler = request.Spoiler,
                Subcategoria = request.Subcategoria,
                DadosActivados = request.DadosActivados,
                IdUnicoAtivado = request.IdUnicoAtivado,
                Encuesta = request.Encuesta ?? [],
                File = request.File is not null ? new FormFileImplementation(request.File) : null,
                Embed = request.Embed is not null ? new EmbedFile(request.Embed) : null
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpDelete("eliminar/{hilo:guid}")]
        public async Task<IResult> Eliminar(Guid hilo)
        {
            var result = await sender.Send(new EliminarHiloCommand(hilo));

            return result.IsSuccess ?
                Results.NoContent()
                :
                result.HandleFailure();

        }

        [HttpPost("establecer-sticky/{hilo:guid}")]
        public async Task<IResult> EstablecerSticky(Guid hilo)
        {
            var result = await sender.Send(new EstablecerStickyCommand(hilo));

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpDelete("eliminar-sticky/{hilo:guid}")]
        public async Task<IResult> EliminarSticky(Guid hilo)
        {
            var result = await sender.Send(new EliminarStickyCommand(hilo));

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpPost("denunciar/{hilo:guid}")]
        public async Task<IResult> Denunciar(Guid hilo)
        {
            var result = await sender.Send(new DenunciarHiloCommand(hilo));

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();

        }

        [HttpPost("colecciones/seguidos/seguir/{hilo:guid}")]
        public async Task<IResult> Seguir(Guid hilo)
        {
            var result = await sender.Send(
                new SeguirHiloCommand()
                {
                    Hilo = hilo
                }
            );

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpPost("colecciones/ocultos/ocultar/{hilo:guid}")]
        public async Task<IResult> Ocultar(Guid hilo)
        {
            var result = await sender.Send(new OcultarHiloCommand()
            {
                Hilo = hilo
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpPost("colecciones/favoritos/poner-en-favoritos/{hilo:guid}")]
        public async Task<IResult> PonerEnFavoritos(Guid hilo)
        {
            var result = await sender.Send(new PonerHiloEnFavoritoCommand()
            {
                Hilo = hilo
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpGet("{hilo:guid}")]
        [ProducesResponseType(typeof(GetHiloResponse), StatusCodes.Status200OK)]
        public async Task<IResult> GetHilo(Guid hilo)
        {
            var result = await sender.Send(new GetHiloQuery()
            {
                Hilo = hilo
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpGet("portadas")]
        [ProducesResponseType(typeof(List<GetHiloPortadaResponse>), StatusCodes.Status200OK)]
        public async Task<IResult> GetPortadas([FromQuery] GetPortadasRequest request)
        {
            var result = await sender.Send(
                new GetHiloPortadasQuery()
                {
                    Titulo = request.Titulo,
                    Categoria = request.Categoria,
                    UltimoBump = request.UltimoBump,
                }
            );

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }


        [HttpGet("colecciones/favoritos")]
        public async Task<IResult> GetHilosFavoritos()
        {
            var result = await sender.Send(new GetColeccionQuery()
            {
                Tipo = TipoDeColeccion.Favoritos
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }
        [HttpGet("colecciones/ocultos")]
        public async Task<IResult> GetHilosOcultos()
        {
            var result = await sender.Send(new GetColeccionQuery()
            {
                Tipo = TipoDeColeccion.Ocultos
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpGet("colecciones/seguidos")]
        public async Task<IResult> GetHilosSeguidos()
        {
            var result = await sender.Send(new GetColeccionQuery()
            {
                Tipo = TipoDeColeccion.Seguidos
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }
    }
    public class PostearHiloRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Guid Subcategoria { get; set; }
        public IFormFile? File { get; set; }
        public string? Embed { get; set; }
        public List<string>? Encuesta { get; set; }
        public bool Spoiler { get; set; }
        public bool DadosActivados { get; set; }
        public bool IdUnicoAtivado { get; set; }
    }

    public class GetPortadasRequest
    {
        public Guid? Categoria { get; set; }
        public string? Titulo { get; set; }
        public DateTime UltimoBump { get; set; }
        public List<Guid> CategoriasFiltradas { get; set; } = [];
}
}