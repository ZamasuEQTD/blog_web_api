using Application.Notificaciones.Commands;
using Application.Notificaciones.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    [Authorize]
    [Route("api/notificaciones")]
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
            Results.NoContent()
                :
            result.HandleFailure();
        }       

        [HttpPost("leer-todas")] 
        public async Task<IResult> LeerTodasLasNotificaciones(){
            LeerNotificacionesCommand command = new LeerNotificacionesCommand();

            var result = await _sender.Send(command);

            return result.IsSuccess ?
            Results.NoContent()
                :
            result.HandleFailure();
        }       
        
        [HttpGet("mis-notificaciones")]
        public async Task<IResult> GetNotificaciones(){
            var result = await _sender.Send(new GetNotificacionesQuery());
           
             return result.IsSuccess ?
            Results.Ok(result)
                :
            result.HandleFailure();
        }
    }
}