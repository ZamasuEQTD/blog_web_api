using SharedKernel.Abstractions;

namespace Domain.Comentarios.Rules {
    
    public class ComentarioDebeEstarActivoRule : IBusinessRule {
        private readonly Comentario.ComentarioStatus _status;

        public ComentarioDebeEstarActivoRule(Comentario.ComentarioStatus status)
        {
            _status = status;
        }

        public string Message => "Comentario debe estar activo";

        public bool IsBroken() => _status != Comentario.ComentarioStatus.Activo;
         
    }
}