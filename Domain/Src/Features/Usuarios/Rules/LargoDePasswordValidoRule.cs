using SharedKernel.Abstractions;

namespace Domain.Usuarios.Rules
{
    public class LargoDePasswordValidoRule : IBusinessRule {
        private readonly string _password;
        public LargoDePasswordValidoRule(string password)
        {
            _password = password;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken()
        {
            throw new NotImplementedException();
        }
    }
}