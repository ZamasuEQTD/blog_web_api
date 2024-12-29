using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Abstractions;
using Application.Abstractions.Data;
using Dapper;
using Domain.Baneos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Atributos
{
    public class SinBaneoFilter :IAsyncActionFilter
    {
        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _user;
        public SinBaneoFilter(IDBConnectionFactory connection, IUserContext user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            using var connection = _connection.CreateConnection();
            string sql = $@"
                    SELECT
                        baneo.id,
                        baneo.mensaje,
                        baneo.concluye,
                        baneo.razon,
                        moderador.moderador
                    FROM baneos baneo
                    JOIN usuarios moderador ON baneo.moderador_id = moderador.id
                    WHERE  baneo.usuario_baneado_id = @UsuarioId
                ";

            GetBaneoResponse? baneo = await connection.QueryFirstOrDefaultAsync<GetBaneoResponse?>(sql, new
            {
                _user.UsuarioId
            });

            if (baneo is not null)
            {
                context.HttpContext.Response.StatusCode = 403;

                await context.HttpContext.Response.WriteAsJsonAsync(new ProblemDetails()
                {
                    Title = "Baneos.HasSidoBaneado",
                    Detail = "No puedes realizar esta accion baneado",
                    Status = 403,
                    Extensions = {
                        {"baneo", baneo},
                    }
                });

                return;
            }

            await next();
        }
    }

    public class GetBaneoResponse
    {
        public Guid Id {get;set;}
        public string Moderador{get;set;}
        [JsonPropertyName("concluye_en")]
        public DateTime? Concluye {get;set;}
        public string? Mensaje {get;set;}
        public Razon Razon {get;set;}
    }
}