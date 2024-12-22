using Application.Categorias.Queries;
using Application.Features.Categorias.Commands.SeedCategorias;
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
        [ProducesResponseType(typeof(List<GetCategoriaReponse>), StatusCodes.Status200OK)]
        public async Task<IResult> GetCategorias()
        {
            var result = await sender.Send(new GetCategoriasQuery());
            return result.IsSuccess ?
            Results.Ok(result)
            :
            result.HandleFailure();
        }
        [HttpPost]
        public async Task<IResult> SeedCategorias()
        {
           
            var result = await sender.Send(new SeedCategoriasCommand());
            return result.IsSuccess ?
                Results.Ok(result)
                :
            result.HandleFailure();
        }
    }
 
}