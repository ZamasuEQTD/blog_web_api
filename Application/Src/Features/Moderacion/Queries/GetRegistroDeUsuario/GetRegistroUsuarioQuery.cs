using System.Text.Json.Serialization;
using Application.Abstractions.Messaging;
using Application.Baneos.Queries;

namespace Application.Moderacion
{
    public class GetRegistroUsuarioQuery : IQuery<GetRegistroUsuarioResponse>
    {
        public Guid Usuario { get; set; }
    }

    public class GetRegistroUsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        [JsonPropertyName("registrado_en")]
        public DateTime RegistradoEn { get; set; }
        public GetBaneoResponse? UltimoBaneo { get; set; }
    }
}