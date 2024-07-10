
using Application.Usuarios;
using Application.Usuarios.Commands;
using Ardalis.ApiEndpoints;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using WebApi.Extensions;

namespace WebApi.Endpoints.Auth
{
    public class Login : EndpointBaseAsync.WithRequest<LoginRequestDto>.WithResult<IResult>
    {
        private readonly ISender _sender;

        public Login(ISender sender)
        {
            _sender = sender;
        }
        [HttpPost("api/auth/login")]
        [SwaggerOperation(Tags = new[] { "Auth" })]
        public override async Task<IResult> HandleAsync(LoginRequestDto request, CancellationToken cancellationToken = default)
        {
            var command = new LoginCommand(
                request.UserName,
                request.Password
            );
            var result = await _sender.Send(command);
            return  result.IsSuccess? 
                Results.Ok(result) 
                :
                result.HandleFailure();
        }
    }
}