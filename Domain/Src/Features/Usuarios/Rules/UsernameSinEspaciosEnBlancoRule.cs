namespace Domain.Usuarios.Rules
{
    public class UsernameSinEspaciosEnBlancoRule : TextoSinEspaciosEnBlancoRule {
        public UsernameSinEspaciosEnBlancoRule(string text) : base("", text) {}
    }
}