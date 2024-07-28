using Application.Comentarios.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;

namespace WebApi.Comentarios.Endpoints
{
    public class EliminarComentario : EndpointBaseAsync.WithRequest<EliminarComentarioRequest>.WithResult<IResult>
    {
        private readonly ISender _sender;
        public EliminarComentario(ISender sender)
        {
            _sender = sender;
        }
        
        [Authorize]
        [HttpDelete("api/comentarios/eliminar/comentario/{comentario}/hilo/{hilo}")]
        [SwaggerOperation(Tags = new[] { "cometarios" })]
        public async override Task<IResult> HandleAsync( EliminarComentarioRequest request, CancellationToken cancellationToken = default)
        {
            var command = new EliminarComentarioCommand(request.Comentario,request.Hilo);
            var result = await _sender.Send(command);
            return result.IsSuccess ?
                Results.NoContent()
                :
                result.ToProblem();
        }
    }
        public class EliminarComentarioRequest {
            [FromRoute(Name = "hilo")]
            public Guid Hilo {get; set; }
            [FromRoute(Name = "comentario")]
            public Guid Comentario {get; set; }
        }
}