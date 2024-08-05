using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;

namespace Application.Bneos.Commands {
    public class DesbanearUsuarioCommandHandler : ICommandHandler<DesbanearUsuarioCommand>
    {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DesbanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository)
        {
            _unitOfWork = unitOfWork;
            _usuariosRepository = usuariosRepository;
        }

        public async Task Handle(DesbanearUsuarioCommand request, CancellationToken cancellationToken) 
        {
            Usuario? usuario = await  _usuariosRepository.GetUsuarioById(new (request.Usuario));

            if(usuario is not Anonimo anon){
                return;
            }

            anon.Desbanear();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}