using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Abstractions;
using Application.Abstractions.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using SharedKernel.Abstractions;

namespace WebApi.Atributos
{
    public class SinBaneo : IAsyncActionFilter
    {
        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _user;
        private readonly IDateTimeProvider _time;
        public SinBaneo(IDBConnectionFactory connection, IUserContext user, IDateTimeProvider time)
        {
            _connection = connection;
            _user = user;
            _time = time;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using (var connection = _connection.CreateConnection())
            {
                string sql = $@"
                    SELECT
                        baneo.mensaje
                    FROM baneos baneo
                    WHERE baneo.usuario_baneado_id = '{_user.UsuarioId}'
                    AND (baneo.concluye IS NULL OR baneo.concluye > @now)
                ";

                Console.WriteLine("sql es: " + sql);

                GetBaneoResponse? baneo = await connection.QueryFirstOrDefaultAsync<GetBaneoResponse?>(sql, new { now = _time.UtcNow });

                if(baneo is not null){
                    context.HttpContext.Response.StatusCode = StatusCodes.Status203NonAuthoritative;

                    context.Result = new BadRequestObjectResult(baneo);

                    return;
                }
            }
            await next();
        }
    }
    public class GetBaneoResponse
    {
        public Guid Id {get;set;}
        public string Moderador{get;set;}
        [JsonPropertyName("concluye_at")]
        public DateTime? Concluye{get;set;}
        public string? Mensaje {get;set;}
    }
}