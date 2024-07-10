using Domain.Common.Services;
using Domain.Usuarios.Failures;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios.Validations
{
    public class ValidarLargoDeUsernameHandler : ValidationHandler {
        private readonly string _username;
        public ValidarLargoDeUsernameHandler(string username)
        {
            _username = username;
        }

        public override Result Handle()
        {
            if (EsLargoInvalido())
            {
                return UsernameFailures.LARGO_INVALIDO;
            }
            return base.Handle();
        }

        private bool EsLargoInvalido()
        {
            return _username.Length > Username.MAXIMO_LENGTH || _username.Length < Username.MIN_LENGTH;
        }
    }

    public class ValidarEspaciosEnBlanco : ValidationHandler {
        private readonly string _username;
        public ValidarEspaciosEnBlanco(string username)
        {
            _username = username;
        }

        public override Result Handle()
        {
            if(StringUtils.ContieneEspaciosEnBlanco(_username)){
                return UsernameFailures.TIENE_ESPACIOS_EN_BLANCO;
            }
            return base.Handle();
        }
    }
}