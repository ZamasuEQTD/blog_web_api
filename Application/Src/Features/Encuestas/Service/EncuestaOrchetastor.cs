using Domain.Encuestas;
using SharedKernel;

namespace Application.Encuestas.Services
{
    public class EncuestaOrchetastor {
        public Result<Encuesta> Orquestar(
            EncuestaId id,
            List<string> respuestas
        ){
            List<Respuesta> r = [];
            
            foreach (var respuesta in respuestas) {
                var result = new Respuesta(new(Guid.NewGuid()),id, respuesta);

                r.Add(result);
            }

            return Encuesta.Create(
                id,
                r
            );
        }
    }
}