using Application.Abstractions.Messaging;

namespace Application.Moderacion
{
    public class VerUsuarioQuery : IQuery<VerUsuarioResponse>
    {
        public Guid Usuario { get; set; }
    }

    public class VerUsuarioResponse
    {
        public Guid Id { get; set; }
        public string Nombre { get; set; }
        public DateTime RegistradoEn { get; set; }
    }
}