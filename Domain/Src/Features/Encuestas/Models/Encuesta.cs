using System.Security.Cryptography.X509Certificates;
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
        
        public Encuesta(List<Respuesta> respuestas) : base(){
            this.Id = new EncuestaId(Guid.NewGuid());
            this.Respuestas = respuestas;
            this.Votos = [];
        }

        public void Votar(UsuarioId usuarioId, RespuestaId respuestaId) {
            this.CheckRule(new UsuarioSoloPuedeVotarUnaVezRule(this,usuarioId));
            this.CheckRule(new RespuestaDebeExistirRule(respuestaId,this));
            
            Votos.Add(new Voto(
                usuarioId,
                respuestaId
            ));
        }

        static public  Encuesta  Create(
            List<Respuesta> respuestas
        ){
            return new Encuesta(
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

    public class UsuarioSoloPuedeVotarUnaVezRule : IBusinessRule {
        private readonly Encuesta _encuesta;
        private readonly UsuarioId _usuario;
        public UsuarioSoloPuedeVotarUnaVezRule(Encuesta encuesta, UsuarioId usuario)
        {
            _encuesta = encuesta;
            _usuario = usuario;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => _encuesta.HaVotado(_usuario);
    }

    public class RespuestaDebeExistirRule : IBusinessRule
    {
        private readonly RespuestaId _id;
        private readonly Encuesta _encuesta;

        public RespuestaDebeExistirRule(RespuestaId id, Encuesta encuesta)
        {
            _id = id;
            _encuesta = encuesta;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => !_encuesta.ContieneRespuesta(_id);
    }
}