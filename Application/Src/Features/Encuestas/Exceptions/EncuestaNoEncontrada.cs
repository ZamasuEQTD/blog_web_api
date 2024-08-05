
namespace Application.Encuestas
{
    public class EncuestaNoEncontrada : InvalidCommandException {

        
        public EncuestaNoEncontrada() : base(["Encuesta no hallada"]){}
    }
}