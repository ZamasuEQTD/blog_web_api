using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using SharedKernel;

namespace Application.Usuarios.Commands
{
    public class RegistroCommandHandler : ICommandHandler<RegistroCommand,   string > {
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        public RegistroCommandHandler(IUsuariosRepository usuariosRepository, IUnitOfWork unitOfWork, IJwtProvider jwtProvider, IPasswordHasher passwordHasher)
        {
            _usuariosRepository = usuariosRepository;
            _unitOfWork = unitOfWork;
            _jwtProvider = jwtProvider;
            _passwordHasher = passwordHasher;
        }

        public async Task<string> Handle(RegistroCommand request, CancellationToken cancellationToken) {
            var username = Username.Create(request.Username);

            if(await _usuariosRepository.UsernameEstaOcupado(username)) throw new ApplicationException("");

            var password = Password.Create(request.Password);            
            
            Anonimo usuario = new Anonimo(
                username,
                _passwordHasher.Hash(password)
            );
            
            _usuariosRepository.Add(usuario);

            await _unitOfWork.SaveChangesAsync();
 
            return _jwtProvider.Generar(usuario);
        }
    }
}