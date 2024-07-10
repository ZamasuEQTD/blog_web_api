using Application.Hilos;
using Application.Hilos.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using SharedKernel;
using SharedKernel.Abstractions;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;
namespace WebApi.Hilos.Endpoints
{
    public class CrearHilo : EndpointBaseAsync.WithRequest<CrearHiloRequest>.WithResult<IResult>
    {
        private readonly ISender _sender;
        public CrearHilo(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("api/hilos/crear")]
        [SwaggerOperation(Tags = new[] { "Hilos" })]
        [ProducesResponseType(typeof(Guid), StatusCodes.Status200OK)]
        public override async Task<IResult> HandleAsync([FromForm] CrearHiloRequest request, CancellationToken cancellationToken = default)
        {
            var command = new CrearHiloCommand(
                request.Titulo,
                request.Descripcion
            );

            var result = await _sender.Send(command,cancellationToken);
            return result.IsSuccess ?
                Results.Ok(result.Value)
                :
                result.HandleFailure();
        }
    }
    public record CrearHiloRequest(
        string Titulo,
        string Descripcion
    );
}