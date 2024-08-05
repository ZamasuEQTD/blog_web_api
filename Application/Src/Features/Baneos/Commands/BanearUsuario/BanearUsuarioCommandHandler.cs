using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Usuarios.Exceptions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;

namespace Application.Bneos.Commands 
{
    public class BanearUsuarioCommandHandler : ICommandHandler<BanearUsuarioCommand> {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IUsuariosRepository usuariosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _usuariosRepository = usuariosRepository;
        }

        public async Task Handle(BanearUsuarioCommand request, CancellationToken cancellationToken) {
            Usuario? usuario =  await _usuariosRepository.GetUsuarioById(new(request.UsuarioId));

            if(usuario is not Anonimo anon) throw new NoEsUsuarioAnonimoException();

            anon.Banear(
                new(_context.UsuarioId),
                request.Mensaje
            );

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}