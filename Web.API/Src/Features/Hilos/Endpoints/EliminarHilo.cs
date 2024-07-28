using Application.Hilos;
using Application.Hilos.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;

namespace WebApi.Hilos.Endpoints
{
    public class EliminarHilo : EndpointBaseAsync.WithRequest<Guid>.WithResult<IResult>
    {
        private readonly ISender _sender;
        public EliminarHilo(ISender sender)
        {
            _sender = sender;
        }

        [Authorize]
        [HttpDelete("api/hilos/eliminar/{hiloId}")]
        [SwaggerOperation(Tags = new[] { "Hilos" })]
        public async override Task<IResult> HandleAsync([FromRoute(Name = "hiloId")] Guid request, CancellationToken cancellationToken = default)
        {
            var command = new EliminarHiloCommand(request);
            var result = await _sender.Send(command);
            return result.IsSuccess ?
                Results.NoContent()
                :
                result.ToProblem();
        }
    }
} 