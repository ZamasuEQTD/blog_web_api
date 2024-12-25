using System.Text.Json.Serialization;
using Application.Bneos.Commands;
using Domain.Baneos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;

namespace WebApi.Controllers    
{

    [Authorize(Roles = "Moderador")]
    [Route("api/baneos")]
    public class BaneosController : Controller
    {
        private readonly ISender _sender;

        public BaneosController(ISender sender)
        {
            _sender = sender;
        }


        [HttpPost("banear")]
        public async Task<IResult> BanearUsuario([FromBody] BanearUsuarioRequest request)
        {
            var command = new BanearUsuarioCommand(){
                UsuarioId = request.UsuarioId,
                Mensaje = request.Mensaje,
                Duracion = request.Duracion
            };

            var result = await _sender.Send(command);

           return result.IsSuccess ?
                Results.NoContent()
                :
                result.HandleFailure();
        }


        [HttpDelete("desbanear/usuario/{usuario}")]
        public async Task<IResult> DesbanearUsuario(Guid usuario)  
        {
            var command = new DesbanearUsuarioCommand(){
                Usuario = usuario
            };
        
            var result = await _sender.Send(command);

            return result.IsSuccess ?
                Results.NoContent()
                :
                result.HandleFailure();
        }
    }

    public class BanearUsuarioRequest
    {
        [JsonPropertyName("usuario_id")]
        public Guid UsuarioId { get; set; }
        public string? Mensaje { get; set; }
        public DuracionBaneo Duracion { get; set; }
    }
}