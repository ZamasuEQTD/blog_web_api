using Application.Abstractions;
using Application.Abstractions.Data;
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
    public class RegistroCommandHandlerTests
    {
        private RegistroCommandHandler _handler;
        private IUsuariosRepository _usuariosRepository;
        private IUnitOfWork _unitOfWork;
        private IJwtProvider _jwtProvider;
        private IPasswordHasher _passwordHasher;

        [SetUp]
        public void Setup()
        {
            _usuariosRepository = Substitute.For<IUsuariosRepository>();
            _unitOfWork = Substitute.For<IUnitOfWork>();
            _jwtProvider = Substitute.For<IJwtProvider>();
            _passwordHasher = Substitute.For<IPasswordHasher>();
            _handler = new RegistroCommandHandler(
                _usuariosRepository,
                _unitOfWork,
                _jwtProvider,
                _passwordHasher
            );
        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Username_Es_Invalido()
        {
            // Arrange
            var command = new RegistroCommand("usuario_invalido@", "password123"); // Username inválido

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            // Puedes verificar el tipo específico de error si es necesario
        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Password_Es_Invalido()
        {
            // Arrange
            var command = new RegistroCommand("usuarioValido", "pass"); // Password inválido

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            // Puedes verificar el tipo específico de error si es necesario
        }

        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Username_Esta_Ocupado()
        {
            // Arrange
            var command = new RegistroCommand("usuarioExistente", "password123");
            _usuariosRepository.UsernameEstaOcupado(Arg.Any<Username>()).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameOcupado);
        }

    }
}