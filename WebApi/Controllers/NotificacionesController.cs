using Application.Notificaciones.Commands;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    [Authorize]
    public class NotificacionesController :  Controller
    {
        private readonly ISender _sender;

        public NotificacionesController(ISender sender)  
        {
            _sender = sender;
        }

        [HttpPost("leer/{id}")] 
        public async Task<IResult> LeerNotificacion(Guid id){
            LeerNotificacionCommand command = new LeerNotificacionCommand(){
                Notificacion = id
            };

            var result = await _sender.Send(command);

            return result.IsSuccess ?
            Results.Ok(result)
                :
            result.HandleFailure();
        }       

        [HttpPost("leer-todas")] 
        public async Task<IResult> LeerTodasLasNotificaciones(){
            LeerNotificacionesCommand command = new LeerNotificacionesCommand();

            var result = await _sender.Send(command);

            return result.IsSuccess ?
            Results.Ok(result)
                :
            result.HandleFailure();
        }       

        [HttpGet()]
        public async Task<IResult> GetNotificaciones(){
        return  Results.Ok();

        }
    }
}