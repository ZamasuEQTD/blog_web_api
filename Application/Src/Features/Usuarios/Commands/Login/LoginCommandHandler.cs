using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Usuarios.Commands {
    public class LoginCommandHandler : ICommandHandler<LoginCommand,  string> {
        private readonly IUsuariosRepository _repository;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IPasswordHasher hasher, IUsuariosRepository repository, IJwtProvider jwtProvider)
        {
            _hasher = hasher;
            _repository = repository;
            _jwtProvider = jwtProvider;
        }

        public async Task<string> Handle(LoginCommand request, CancellationToken cancellationToken) {
            Username username;
            
            Password password;
            
            try {
                username = Username.Create(request.Username);

                password = Password.Create(request.Password);
            }
            catch (BusinessRuleValidationException)
            {
                throw new UsernamePasswordInvalida();
            }

            Usuario? usuario = await _repository.GetUsuarioByUsername(username);

            if(usuario is null || !_hasher.Verify(password, usuario.HashedPassword)) throw new UsernamePasswordInvalida();

            return _jwtProvider.Generar(usuario);
        }
    }

    public class UsernamePasswordInvalida : ApplicationException {
        public UsernamePasswordInvalida() : base(""){}
    }
}