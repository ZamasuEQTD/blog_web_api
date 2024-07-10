using Application.Usuarios;
using Application.Usuarios.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;

namespace WebApi.Endpoints.Auth
{
    public class Registro : EndpointBaseAsync.WithRequest<RegistrarUsuarioRequestDto>.WithResult<IResult>
    {
        private readonly ISender _sender;

        public Registro(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("api/auth/registro")]
        [SwaggerOperation(Tags = new[] { "Auth" })]
        public override async Task<IResult> HandleAsync(RegistrarUsuarioRequestDto request, CancellationToken cancellationToken = default)
        {
              var command = new RegistroCommand(
                request.UserName,
                request.Password
            );
            var result = await _sender.Send(command);
            return result.IsSuccess? 
                Results.Ok(result) 
                :
                result.HandleFailure() ;
        }
    }
}