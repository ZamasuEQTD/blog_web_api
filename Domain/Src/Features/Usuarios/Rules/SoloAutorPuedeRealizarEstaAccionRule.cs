using SharedKernel.Abstractions;

namespace Domain.Usuarios.Rules
{
    public class SoloAutorPuedeRealizarEstaAccionRule : IBusinessRule {
        private readonly UsuarioId _autorId;
        private readonly UsuarioId _usuarioId;

        public SoloAutorPuedeRealizarEstaAccionRule(UsuarioId usuarioId, UsuarioId autorId)
        {
            _usuarioId = usuarioId;
            _autorId = autorId;
        }

        public string Message => "Solo el autor puede realizar esta acción";

        public bool IsBroken()=> _autorId != _usuarioId;
    }
}