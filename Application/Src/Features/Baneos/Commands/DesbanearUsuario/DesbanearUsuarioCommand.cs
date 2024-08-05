using Application.Abstractions.Messaging;

namespace Application.Bneos.Commands
{
    public class DesbanearUsuarioCommand : ICommand
    {
        public Guid Usuario  { get; set; }
    }
}