using Application.Comentarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    public class ComentariosController : ApiController
    {
        public ComentariosController(ISender sender) : base(sender)
        {
        }
        [HttpPost(":hilo/destacar/:comentario")]
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

        [HttpPost("comentar-hilo/:hilo")]
        public async Task<IResult> ComentarHilo(
            [FromQuery] Guid hilo,
            [FromBody] ComentarHiloRequest request
        )
        {
            var result = await sender.Send(new ComentarHiloCommand(
                hilo,
                request.Texto
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
    }
}