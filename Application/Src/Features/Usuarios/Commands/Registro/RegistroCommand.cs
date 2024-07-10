using System.Windows.Input;
using Application.Abstractions.Messaging;
using SharedKernel;

namespace Application.Usuarios.Commands
{
    public class RegistroCommand : ICommand< string >
    {
        public string Username { get; set; }
        public string Password { get; set; }

        public RegistroCommand( string username, string password )
        {
            this.Username = username;
            this.Password = password;
        }
    }
}