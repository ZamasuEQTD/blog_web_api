using Application.Abstractions.Messaging;

namespace Application.Bneos.Commands
{
    public class BanearUsuarioCommand : ICommand
    {
        public Guid UsuarioId { get; set; }
        public string Mensaje { get; set; }
        public int Duracion   { get; set; }
    }   
}