using SharedKernel.Abstractions;

namespace Domain.Encuestas
{
    public class Respuesta : Entity {

        public RespuestaId Id {get;private set;}
        public string Contenido {get;private set;}
        public EncuestaId EncuestaId{ get; private set; }

        private Respuesta( )
        {
            
        }

        public Respuesta(
            RespuestaId id,
            EncuestaId encuestaId,
            string contenido
        )
        {
            Id = id;
            EncuestaId = encuestaId;
            Contenido = contenido;
        }
    }
    
}