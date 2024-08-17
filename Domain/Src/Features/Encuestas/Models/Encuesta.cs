using System.Security.Cryptography.X509Certificates;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class Encuesta : Entity<EncuestaId>
    {
        public List<Respuesta> Respuestas { get; private set; }
        public List<Voto> Votos { get; private set; }
        public bool HaVotado(UsuarioId id) => Votos.Any(v => v.VotanteId == id);
        public bool ContieneRespuesta(RespuestaId id) => Respuestas.Any(r => r.Id == id);

        private Encuesta() { }

        public Encuesta(List<Respuesta> respuestas) : base()
        {
            this.Id = new EncuestaId(Guid.NewGuid());
            this.Respuestas = respuestas;
            this.Votos = [];
        }

        static public Encuesta Create(
            List<Respuesta> respuestas
        )
        {
            return new Encuesta(
                respuestas
            );
        }
    }


}