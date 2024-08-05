namespace Domain.Usuarios.Rules {
    public class PasswordSinEspaciosEnBlancoRule : TextoSinEspaciosEnBlancoRule {
        public PasswordSinEspaciosEnBlancoRule(string text) : base("", text) {}
    }
}