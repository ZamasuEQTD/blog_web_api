using Application.Abstractions;
using Application.Usuarios.Commands;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.ValueObjects;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Application.Usuarios.Commands
{
    [TestFixture]
    public class LoginCommandHandlerTests
    {
        private LoginCommandHandler _handler;
        private IUsuariosRepository _repository;
        private IPasswordHasher _hasher;
        private IJwtProvider _jwtProvider;

        [SetUp]
        public void Setup()
        {
            _repository = Substitute.For<IUsuariosRepository>();
            _hasher = Substitute.For<IPasswordHasher>();
            _jwtProvider = Substitute.For<IJwtProvider>();
            _handler = new LoginCommandHandler(_hasher, _repository, _jwtProvider);
        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Username_Es_Invalido()
        {
            // Arrange
            var command = new LoginCommand("usuario_invalido@", "password123");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameOrPasswordIncorrecta);

        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Password_Es_Invalido()
        {
            // Arrange
            var command = new LoginCommand("usuarioValido", "pass");

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameOrPasswordIncorrecta);

        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Usuario_No_Existe()
        {
            // Arrange
            var command = new LoginCommand("usuarioNoExistente", "password123");
            _repository.GetUsuarioByUsername(Arg.Any<Username>()).Returns((Usuario?)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameOrPasswordIncorrecta);

        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Password_Es_Incorrecto()
        {
            // Arrange
            var command = new LoginCommand("usuarioExistente", "passwordIncorrecto");
            var usuario = new Anonimo(
                Username.Create("codubiiii").Value,
                "password");
            _repository.GetUsuarioByUsername(Arg.Any<Username>()).Returns(usuario);
            _hasher.Verify(Arg.Any<Password>(), usuario.HashedPassword).Returns(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameOrPasswordIncorrecta);

        }


    }
}