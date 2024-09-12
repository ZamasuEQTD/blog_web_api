using System.Security.Cryptography.X509Certificates;
using Domain.Comentarios;
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

        public Result Votar(UsuarioId usuarioId, RespuestaId respuestaId)
        {
            if (HaVotado(usuarioId)) return EncuestaFailures.YaHaVotado;

            if (!ContieneRespuesta(respuestaId)) return EncuestaFailures.RespuestaInexistente;

            Votos.Add(new Voto(
                usuarioId,
                respuestaId
            ));

            return Result.Success();
        }


        static public Result<Encuesta> Create(
            List<string> respuestas
        )
        {
            List<Respuesta> _respuestas = [];

            foreach (var r in respuestas)
            {
                _respuestas.Add(new Respuesta(r));
            }

            return new Encuesta(
                _respuestas
            );
        }

    }


}