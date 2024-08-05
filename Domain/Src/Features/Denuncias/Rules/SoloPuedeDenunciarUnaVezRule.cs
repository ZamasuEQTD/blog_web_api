using Domain.Usuarios;
using SharedKernel.Abstractions;

namespace Domain.Denuncias.Rules {
    public class SoloPuedeDenunciarUnaVezRule : IBusinessRule {

        private readonly UsuarioId _usuarioId;
        private readonly IEnumerable<Denuncia> _denuncias;

        public SoloPuedeDenunciarUnaVezRule(IEnumerable<Denuncia> denuncias, UsuarioId usuarioId)
        {
            _denuncias = denuncias;
            _usuarioId = usuarioId;
        }

        public string Message => "Solo puedes denunciar una vez";

        public bool IsBroken()=> _denuncias.Any(d=> d.DenuncianteId == _usuarioId);
    }
}