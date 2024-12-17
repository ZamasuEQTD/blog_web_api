using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Baneos.Queries;
using Domain.Baneos;

namespace Application.Moderacion
{
    public class GetRegistroUsuarioQuery : IQuery<GetRegistroUsuarioResponse>
    {
        public Guid Usuario { get; set; }

        public GetRegistroUsuarioQuery(Guid usuario)
        {
            Usuario = usuario;
        }
    }

    public class GetRegistroUsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        [JsonPropertyName("registrado_en")]
        public DateTime RegistradoEn { get; set; }
    }
}