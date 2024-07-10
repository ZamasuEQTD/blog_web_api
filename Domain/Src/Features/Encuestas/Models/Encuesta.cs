using Domain.Encuestas.Rules;
using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class Encuesta : Entity<EncuestaId> {
        public List<Respuesta> Respuestas {get;private set;}
        public List<Voto> Votos {get;private set;}
        public bool HaVotado(UsuarioId id) => Votos.Any(v=> v.VotanteId == id);
        public bool ContieneRespuesta(RespuestaId id) => Respuestas.Any(r=> r.Id == id);
        private Encuesta(){}
        public Encuesta(EncuestaId id, List<Respuesta> respuestas) : base(id){
            this.Id = id;
            this.Respuestas = respuestas;
            this.Votos = [];
        }
        public Result Votar(UsuarioId usuarioId, RespuestaId respuestaId) {
            ValidationHandler handler = new RespuestaDebeExistirRule(respuestaId,this);
            handler
            .SetNext(new UsuarioSoloPuedeVotarUnaVezEnEncuestaRule(usuarioId, this));

            var result = handler.Handle();

            if(result.IsFailure) return result;

            Votos.Add(
                new Voto(
                    new VotoId(Guid.NewGuid()), usuarioId, respuestaId
                )
            );

            return Result.Success();
        }


        static public Result<Encuesta> Create(
            EncuestaId id,
            List<Respuesta> respuestas
        ){
            return new Encuesta(
                id,
                respuestas
            );
        }
    }

    public class EncuestaDebeTenerAlMenosCantidadDeRespuestas : IBusinessRule {
        private readonly List<string> _respuestas;

        public EncuestaDebeTenerAlMenosCantidadDeRespuestas(List<string> respuestas)
        {
            _respuestas = respuestas;
        }
        public string Message => "Cantidad invalida de respuestas";
        public bool IsBroken() => _respuestas.Count > 5 || _respuestas.Count < 2;
    }

}