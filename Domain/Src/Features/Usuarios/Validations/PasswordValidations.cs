using Domain.Common.Services;
using Domain.Usuarios.Failures;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Usuarios.Validations
{
    public class PasswordSinEspaciosEnBlancos : ValidationHandler
    {

        private readonly string password;

        public PasswordSinEspaciosEnBlancos(string password)
        {
            this.password = password;
        }

        public override Result Handle()
        {
            if(StringUtils.ContieneEspaciosEnBlanco(password)){
                return PasswordFailures.TIENE_ESPACIOS_EN_BLANCO;
            }

            return base.Handle();
        }
    }

    public class ValidarLargoDePasswordHandler : ValidationHandler {
        private readonly string _username;
        public ValidarLargoDePasswordHandler(string username)
        {
            _username = username;
        }

        public override Result Handle()
        {
            if (EsLargoInvalido())
            {
                return PasswordFailures.LARGO_INVALIDO;
            }
            return base.Handle();
        }

        private bool EsLargoInvalido()
        {
            return _username.Length > Username.MAXIMO_LENGTH || _username.Length < Username.MIN_LENGTH;
        }
    }
}