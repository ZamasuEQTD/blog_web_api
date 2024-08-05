using Application.Abstractions.Messaging;

namespace Application.Usuarios.Commands
{
    public class LoginCommand : ICommand<string>
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public LoginCommand(string username, string password)
        {
            this.Username = username;
            this.Password = password;
        }
    }
}