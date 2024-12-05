using Application.Abstractions.Messaging;

namespace Application.Moderacion.Queries
{
    public class GetRegistroDeHilosQuery : IQuery<IEnumerable<GetRegistroDeHiloResponse>>
    {
        public Guid Usuario { get; set; }
        public DateTime? UltimoHilo { get; set; }
    }

    public class GetRegistroDeHiloResponse
    {
        public DateTime Fecha { get; set; }
        public string Contenido { get; set; }
        public GetHiloRegistroResponse Hilo { get; set; }
    }
}