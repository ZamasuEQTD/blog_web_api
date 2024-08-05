using SharedKernel.Abstractions;

namespace Domain.Usuarios.Rules
{
    public class LargoDeUsernameDebeSerValidoRule : IBusinessRule {

        private readonly string _username;
        public string Message =>"El nombre de usuario debe tener entre";
        public LargoDeUsernameDebeSerValidoRule(string username)
        {
            _username = username;
        }

        public bool IsBroken() => EsLargoInvalido();

        private bool EsLargoInvalido()
        {
            return _username.Length > Username.MAXIMO_LENGTH || _username.Length < Username.MIN_LENGTH;
        }
    }
}