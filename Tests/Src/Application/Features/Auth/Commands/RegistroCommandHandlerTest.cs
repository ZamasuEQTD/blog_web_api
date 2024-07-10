using System.Reflection.Metadata;
using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Usuarios.Commands;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using Domain.Usuarios.Failures;
using FluentAssertions;
using NSubstitute;

namespace Tests.Application.Auth {
    public class RegistroCommandHandlerTest
    {

        private readonly RegistroCommandHandler _handler;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IJwtProvider _jwtProvider;
        private readonly IPasswordHasher _passwordHasher;
        public RegistroCommandHandlerTest() {
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
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_UsernameEstaOcupado() {
            Username username = Username.Create("username").Value;
            
            _usuariosRepository.UsernameEstaOcupado(username).Returns(true);


            var result = await _handler.Handle(new("username","password"), default);


            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.USERNAME_OCUPADO);
        }

        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_UsernameEstaVacio() {
            var result = await _handler.Handle(new("","password"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsernameFailures.LARGO_INVALIDO);
        }
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_UsernameTieneEspaciosEnBlanco() {
            var result = await _handler.Handle(new("user name","password"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsernameFailures.TIENE_ESPACIOS_EN_BLANCO);
        }
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_UsernameEsDemasiadoLargo() {
            var result = await _handler.Handle(new("usernameeeeeeeeee","password"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsernameFailures.LARGO_INVALIDO);
        }

        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_PasswordEstaVacia() {
            var result = await _handler.Handle(new("username",""), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PasswordFailures.LARGO_INVALIDO);
        }
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_PasswordTieneEspaciosEnBlanco() {
            var result = await _handler.Handle(new("username","pass word"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PasswordFailures.TIENE_ESPACIOS_EN_BLANCO);
        }
        [Fact]
        public async Task Handle_DebeRetornarResultFailure_Cuando_PasswordEsDemasiadoLargo() {
            var result = await _handler.Handle(new("username","passwordddddddddddd"), default);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(PasswordFailures.LARGO_INVALIDO);
        }
    }
}