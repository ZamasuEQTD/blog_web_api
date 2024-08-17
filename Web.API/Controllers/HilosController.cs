using Application.Hilos.Commands;
using Application.Hilos.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using WebApi.Extensions;
using WebApi.Infraestructure;

namespace WebApi.Controllers
{
    public class HilosController : ApiController
    {
        public HilosController(ISender sender) : base(sender) { }

        [HttpPost("")]
        public async Task<IResult> Postear([FromForm] PostearHiloRequest request)
        {
            var result = await sender.Send(new PostearHiloCommand()
            {
                Titulo = request.Titulo,
                Descripcion = request.Descripcion,
                Spoiler = request.Spoiler,
                Subcategoria = request.Subcategoria,
                DadosActivados = request.DadosActivados,
                IdUnicoAtivado = request.IdUnicoAtivado
            });

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }

        [HttpDelete]
        public async Task<IResult> Eliminar(Guid hilo)
        {
            var result = await sender.Send(new EliminarHiloCommand(hilo));

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();

        }
        [HttpGet("hilos/portadas")]
        public async Task<IResult> GetPortadas([FromQuery] GetPortadasRequest request)
        {
            var result = await sender.Send(
                new GetPortadasHomeQuery()
                {
                    Titulo = request.Titulo,
                    Categoria = request.Categoria,
                    UltimoBump = request.UltimoBump
                }
            );

            return result.IsSuccess ?
                Results.Ok(result)
                :
                result.HandleFailure();
        }
    }
    public class PostearHiloRequest
    {
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public Guid Subcategoria { get; set; }
        public IFormFile? File { get; set; }
        public string? Embed { get; set; }
        public List<string>? Encuesta { get; set; }
        public bool Spoiler { get; set; }
        public bool DadosActivados { get; set; }
        public bool IdUnicoAtivado { get; set; }
    }

    public class GetPortadasRequest
    {
        public Guid? Categoria { get; set; }
        public string? Titulo { get; set; }
        public DateTime UltimoBump { get; set; }
    }
}