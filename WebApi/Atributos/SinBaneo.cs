using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Application.Abstractions;
using Application.Abstractions.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApi.Atributos
{
    public class SinBaneo : IAsyncActionFilter
    {

        private readonly IDBConnectionFactory _connection;
        private readonly IUserContext _user;
        public SinBaneo(IDBConnectionFactory connection, IUserContext user)
        {
            _connection = connection;
            _user = user;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {

            using (var connection = _connection.CreateConnection())
            {
                string sql = $@"
                    SELECT
                        baneo.id
                        baneo.mensaje
                        baneo.concluye,
                        moderador.nombre_de_moderador
                    FROM baneos baneo
                    JOIN usuarios moderador ON baneo.moderador_id = moderador.id
                    WHERE  baneo.usuario_id = {_user.UsuarioId}
                ";

                string? baneo = await connection.QueryFirstAsync<string?>(sql);

                if(baneo is not null){
                    context.HttpContext.Response.StatusCode = 200;
                }

                await next();
            }
        }
    }

    public class GetBaneoResponse
    {
        public Guid Id {get;set;}
        public string Moderador{get;set;}
        [JsonPropertyName("concluye_en")]
        public DateTime? Concluye{get;set;}
        public string? Mensaje {get;set;}


    }
}