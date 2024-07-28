using Domain.Comentarios.Services;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Validators
{
    public class NoPuedeTaguearMasComentariosQueLaPermitida : ValidationHandler {
        private readonly string _texto;

        public NoPuedeTaguearMasComentariosQueLaPermitida(string texto)
        {
            _texto = texto;
        }

        public override Result Handle()
        {
            if(TagUtils.CantidadDeTags(_texto) > 5){
                return new Error("Comentarios.NoPuedesTagguearMasDeCincoComentarios");
            }
            return base.Handle();
        }
    }
}