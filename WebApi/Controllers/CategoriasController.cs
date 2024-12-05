using Application.Categorias.Commands;
using Application.Categorias.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    [Route("api/categorias")]
    public class CategoriasController : ApiController
    {
        public CategoriasController(ISender sender) : base(sender)
        {
        }

        [HttpGet( )]
        public async Task<IResult> GetCategorias()
        {
            var result = await sender.Send(new GetCategoriasQuery());
            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }
        [HttpPost("ol")]
        public async Task<IResult> CrearCategoria(
            [FromBody] CrearCategoriaRequest request
        )
        {
            var result = await sender.Send(new CrearCategoriaCommand(
                request.Nombre,
                request.Subcategorias.Select(s => new CrearSubcategoriaDto(
                    s.Nombre,
                    s.NombreCorto
                )).ToList()
            ));

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }
    }

    public class CrearCategoriaRequest
    {
        public string Nombre { get; set; }
        public List<CrearSubcategoriaDto> Subcategorias { get; set; }
    }

    public class CrearSubcategoriaRequest
    {
        public string Nombre { get; set; }
        public string NombreCorto { get; set; }
    }
}