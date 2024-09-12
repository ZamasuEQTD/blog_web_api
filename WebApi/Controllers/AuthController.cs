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
        public async Task<IResult> Login(LoginRequest request)
        {
            var command = new LoginCommand(
                request.UserName,
                request.Password
            );

            var result = await _sender.Send(command);

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }


        [HttpPost("api/auth/registro")]
        public async Task<IResult> Registro(RegistroRequest request)
        {
            var command = new RegistroCommand(
                request.UserName,
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
            public string UserName { get; set; }
            public string Password { get; set; }
        }
        public class RegistroRequest
        {
            public string UserName { get; set; }
            public string Password { get; set; }
        }
    }
}