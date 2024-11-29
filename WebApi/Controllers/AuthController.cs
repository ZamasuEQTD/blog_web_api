using Application.Usuarios.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers
{
    public class AuthController : Controller
    {
        private readonly ISender _sender;

        public AuthController(ISender sender)
        {
            _sender = sender;
        }

        [HttpPost("api/auth/login")]
        public async Task<IResult> Login([FromBody] LoginRequest request)
        {
            var command = new LoginCommand(
                request.Username,
                request.Password
            );

            var result = await _sender.Send(command);

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }


        [HttpPost("api/auth/registro")]
        public async Task<IResult> Registro([FromBody] RegistroRequest request)
        {
            var command = new RegistroCommand(
                request.Username,
                request.Password
            );

            var result = await _sender.Send(command);

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
        public class RegistroRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}