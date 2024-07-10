using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.Failures;
using Domain.Usuarios.ValueObjects;
using SharedKernel;

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

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken) {
            var username = Username.Create(request.Username);

            if(username.IsFailure){
                return UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA;
            }

            var password = Password.Create(request.Password);

            if(password.IsFailure){
                return UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA;
            }

            Usuario? usuario = await _repository.GetUsuarioByUsername(username.Value);

            if(usuario is null){
                return UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA;
            }

            if(!_hasher.Verify(password.Value, usuario.HashedPassword)){
                return UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA;
            }

            return _jwtProvider.Generar(usuario);
        }
    }
}