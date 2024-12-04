using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Infraestructure;

namespace WebApi.Controllers;

[Route("api/[controller]")]	
public class ModeracionController : ApiController
{
    public ModeracionController(ISender sender) : base(sender)
    {
    }

    [HttpGet("registro/usuario/{id}")]
    public async Task<IResult> GetRegistroUsuario(string id)
    {
        var result = await sender.Send(new GetRegistroUsuarioQuery(id));

        return result.IsSuccess ? Results.Ok(result) : Results.NotFound();
    }
}