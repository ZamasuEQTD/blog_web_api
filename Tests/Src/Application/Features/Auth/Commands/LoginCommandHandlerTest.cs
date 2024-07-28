using Application.Abstractions;
using Application.Usuarios.Commands;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.Failures;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Tests.Application.Auth.Commands {
    public class LoginCommandHandlerTest {
        private readonly LoginCommandHandler _handler;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtProvider _jwtProvider;

        public LoginCommandHandlerTest() {
            _usuariosRepository = Substitute.For<IUsuariosRepository>();
            _jwtProvider = Substitute.For<IJwtProvider>();
            _passwordHasher = Substitute.For<IPasswordHasher>();

            
            _handler = new LoginCommandHandler(
                _passwordHasher,
                _usuariosRepository,
                _jwtProvider
            );
        }

        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_UsernameNoExiste() {
            Username username = Username.Create("username").Value;
            
            _usuariosRepository.GetUsuarioByUsername(username).ReturnsNull();

            var result = await _handler.Handle(new("username","password"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA);
        }
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_PasswordVacia() {
            var result = await _handler.Handle(new("username",""), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.USERNAME_O_PASSWORD_INVALIDA);
        }
        
    }
}