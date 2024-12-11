using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Usuarios.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IUsuariosRepository _repository;
        private readonly IPasswordHasher _hasher;
        private readonly IJwtProvider _jwtProvider;
        public LoginCommandHandler(IPasswordHasher hasher, IUsuariosRepository repository, IJwtProvider jwtProvider)
        {
            _hasher = hasher;
            _repository = repository;
            _jwtProvider = jwtProvider;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<Username> username = Username.Create(request.Username);
            
            Result<Password> password = Password.Create(request.Password);

            if (username.IsFailure || password.IsFailure) return UsuariosFailures.CredencialesInvalidas;

            Usuario? usuario = await _repository.GetUsuarioByUsername(username.Value);

            if (usuario is null || !_hasher.Verify(password.Value, usuario.HashedPassword)) return UsuariosFailures.CredencialesInvalidas;

            return await _jwtProvider.Generar(usuario);
        }
    }
}