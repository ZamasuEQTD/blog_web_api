using Application.Comentarios.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;

namespace WebApi.Comentarios.Endpoints
{
    public class ComentarHilo : EndpointBaseAsync.WithRequest<ComentarHiloRequest>.WithResult<IResult>
    {
        private readonly ISender _sender;
        public ComentarHilo(ISender sender)
        {
            _sender = sender;
        }
        
        [Authorize]
        [HttpPost("api/comentarios/comentar")]
        [SwaggerOperation(Tags = new[] { "cometarios" })]
        public async override Task<IResult> HandleAsync( ComentarHiloRequest request, CancellationToken cancellationToken = default) {
            var command = new ComentarCommand(
                request.Hilo,
                request.Texto
            );

            var result = await _sender.Send(command);
            return result.IsSuccess ?
                Results.NoContent()
                :
                result.ToProblem();
        }
    }
        public class ComentarHiloRequest {
            public Guid Hilo {get; set; }
            public string Texto {get; set; }
        }
}