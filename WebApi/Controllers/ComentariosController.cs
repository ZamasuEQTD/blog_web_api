using System.Text.Json.Serialization;
using Application.Comentarios;
using Application.Comentarios.Commands;
using Application.Comentarios.GetComentarioDeHilos;
using Application.Features.Comentarios.Queries.GetComentarioDeHilo;
using Infraestructure.Media;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    
    [Route("api/comentarios")]
    public class ComentariosController : ApiController
    {
        public ComentariosController(ISender sender) : base(sender)
        {
        }

        [HttpGet("{hilo:guid}/{tag}")]
        [ProducesResponseType(typeof(IEnumerable<GetComentarioResponse>), StatusCodes.Status200OK)]
        public async Task<IResult> GetComentario(  Guid hilo, string tag)
        {
            var result = await sender.Send(new GetComentarioDeHiloQuery(
                hilo,
                tag
            ));

            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }

        [HttpGet("hilo/{hilo}")]
        [ProducesResponseType(typeof(IEnumerable<GetComentarioResponse>), StatusCodes.Status200OK)]
        public async Task<IResult> GetComentarios([FromRoute] Guid hilo, [FromQuery(Name = "ultimo_comentario")] Guid? ultimoComentario)
        {
            var result = await sender.Send(new GetComentariosDeHiloQuery(){
                HiloId = hilo,
                UltimoComentario = ultimoComentario
            });

            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }

        [Authorize]
        [HttpPost("hilo/{hilo:guid}/destacar/{comentario:guid}")]
        public async Task<IResult> Destacar(Guid hilo, Guid comentario)
        {
            var result = await sender.Send(new DestacarComentarioCommand(
                hilo,
                comentario
            ));

            return result.IsSuccess ?
            Results.NoContent()
            :
            result.HandleFailure();
        }

        [Authorize]
        [HttpPost("comentar-hilo/{hilo:guid}")]
        public async Task<IResult> Comentar(
            [FromRoute] Guid hilo,
            [FromForm] ComentarHiloRequest request
        )
        {
            var result = await sender.Send(new ComentarHiloCommand(
                hilo,
                request.Texto,
                request.EmbedFile is not null ? new EmbedFile(request.EmbedFile!) : null,
                request.File is not null ? new FormFileImplementation(request.File!) : null,
                request.Spoiler
            ));

            return result.IsSuccess ?
            Results.NoContent()
            :
            result.HandleFailure();
        }

        [Authorize(Roles = "Moderador")]
        [HttpDelete("eliminar/hilo/{hilo:guid}/comentario/{comentario:guid}")]
        public async Task<IResult> Eliminar(Guid comentario, Guid hilo)
        {
            var result = await sender.Send(new EliminarComentarioCommand(){
                Comentario = comentario,
                Hilo = hilo
            });

            return result.IsSuccess ?
            Results.NoContent()
            :
            result.HandleFailure();
        }
    }

    public class ComentarHiloRequest
    {
        [JsonPropertyName("texto")]
        public string Texto { get; set; }
        [JsonPropertyName("embed_file")]
        public string? EmbedFile { get; set; }
        [JsonPropertyName("file")]
        public IFormFile? File { get; set; }
        [JsonPropertyName("spoiler")]
        public bool Spoiler {get;set;}
    }
}