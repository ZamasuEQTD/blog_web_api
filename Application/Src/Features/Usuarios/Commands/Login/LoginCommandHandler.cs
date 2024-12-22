using Application.Abstractions;
using Application.Abstractions.Messaging;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using Microsoft.AspNetCore.Identity;
using SharedKernel;

namespace Application.Usuarios.Commands
{
    public class LoginCommandHandler : ICommandHandler<LoginCommand, string>
    {
        private readonly IPasswordHasher _hasher;
        private readonly IJwtProvider _jwtProvider;
        private readonly UserManager<Usuario> _userManager;
        public LoginCommandHandler(IPasswordHasher hasher, IJwtProvider jwtProvider, UserManager<Usuario> userManager)
        {
            _hasher = hasher;
            _jwtProvider = jwtProvider;
            _userManager = userManager;
        }

        public async Task<Result<string>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            Result<Username> username = Username.Create(request.Username);
            
            Result<Password> password = Password.Create(request.Password);

            if (username.IsFailure || password.IsFailure) return UsuariosFailures.CredencialesInvalidas;
            
            Usuario? usuario = await _userManager.FindByNameAsync(request.Username);

            if (usuario is null || !_hasher.Verify(password.Value, usuario.PasswordHash!)) return UsuariosFailures.CredencialesInvalidas;

            return await _jwtProvider.Generar(usuario);
        }
    }
}