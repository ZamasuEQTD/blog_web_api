using Application.Abstractions.Messaging;
using Domain.Baneos;

namespace Application.Bneos.Commands
{
    public class BanearUsuarioCommand : ICommand
    {
        public Guid UsuarioId { get; set; }
        public string? Mensaje { get; set; }
        public DuracionBaneo? Duracion   { get; set; }
    }   
}