using Application.Comentarios;
using Application.Comentarios.Commands;
using Infraestructure.Media;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    
    [Route("comentarios")]
    public class ComentariosController : ApiController
    {
        public ComentariosController(ISender sender) : base(sender)
        {
        }

        [HttpGet("hilo/{hilo}")]
        [ProducesResponseType(typeof(IEnumerable<GetComentarioResponse>), StatusCodes.Status200OK)]
        public async Task<IResult> GetComentarios([FromRoute] Guid hilo, [FromQuery] DateTime? ultimoComentario)
        {
            var result = await sender.Send(new GetComentariosDeHiloQuery(){
                Hilo = hilo,
                UltimoComentario = ultimoComentario
            });

            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }

        [HttpPost("hilo/{hilo}/destacar/{comentario}")]
        public async Task<IResult> Destacar(Guid hilo, Guid comentario)
        {
            var result = await sender.Send(new DestacarComentarioCommand(
                hilo,
                comentario
            ));

            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }

        [HttpPost("comentar-hilo/{hilo}")]
        public async Task<IResult> ComentarHilo(
            [FromRoute] Guid hilo,
            [FromForm] ComentarHiloRequest request
        )
        {
            var result = await sender.Send(new ComentarHiloCommand(
                hilo,
                request.Texto,
                request.EmbedFile is not null ? new EmbedFile(request.EmbedFile!) : null,
                request.File is not null ? new FormFileImplementation(request.File!) : null
            ));

            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }
    }

    public class ComentarHiloRequest
    {
        public string Texto { get; set; }
        public string? EmbedFile { get; set; }
        public IFormFile File { get; set; }
    }
}