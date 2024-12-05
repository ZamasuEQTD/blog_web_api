using System.Text.Json.Serialization;
using Application.Moderacion;
using Application.Moderacion.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infraestructure;

namespace WebApi.Controllers;

[Route("api/moderacion")]	
public class ModeracionController : ApiController
{
    public ModeracionController(ISender sender) : base(sender)
    {
    }

    [HttpGet("registro/hilos/usuario/{id}")]
    [ProducesResponseType(typeof(IEnumerable<GetRegistroDeHiloResponse>), StatusCodes.Status200OK)]	
    public async Task<IResult> GetRegistroDeHilosPosteados(Guid id,  [FromQuery(Name ="ultimo_hilo")] DateTime? UltimoHilo) {
        var result = await sender.Send(new GetRegistroDeHilosQuery(){
            Usuario = id,
            UltimoHilo = UltimoHilo
        });

        return result.IsSuccess ? Results.Ok(result) : Results.NotFound();
    }

    [HttpGet("registro/usuario/{id}")]
    [ProducesResponseType(typeof(GetRegistroUsuarioResponse), StatusCodes.Status200OK)]
    public async Task<IResult> GetRegistroUsuario(Guid id)
    {
        var result = await sender.Send(new GetRegistroUsuarioQuery(id));

        return result.IsSuccess ? Results.Ok(result) : Results.NotFound();
    }
}