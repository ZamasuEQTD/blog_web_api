using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Hilos.Commands;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Usuarios;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;
using Xunit;

namespace Tests.Application.Hilos.Commands
{
    public class DenunciarHiloCommandHandlerTests : HiloBaseCommand
    {
        private DenunciarHiloCommandHandler _handler;

        public DenunciarHiloCommandHandlerTests()
        {
            _handler = new DenunciarHiloCommandHandler(_unitOfWork, _user, _hilosRepository);
        }

        [Fact]
        public async Task Handle_Deberia_Retornar_HilosFailures_NoEncontrado_Si_Hilo_No_Existe()
        {
            // Arrange
            var command = new DenunciarHiloCommand(Guid.NewGuid());
            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns((Hilo?)null);

            // Act 
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.NoEncontrado);

        }
        [Test]
        public async Task Handle_Deberia_Retornar_Error_Si_Ya_Ha_Sido_Denunciado()
        {
            // Arrange
            var command = new DenunciarHiloCommand(Guid.NewGuid());

            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns(_hilo);
            _hilosRepository.HaDenunciado(_hilo.Id, Arg.Any<UsuarioId>()).Returns(true);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.YaHaDenunciado);
            _hilosRepository.DidNotReceive().Add(Arg.Any<DenunciaDeHilo>()); // Verificar que no se agrega la denuncia
            _unitOfWork.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>()); // Verificar que no se guardan cambios
        }

        [Test]
        public async Task Handle_Deberia_Agregar_Denuncia_Y_Guardar_Cambios_Si_Todo_Es_Valido()
        {
            // Arrange
            var command = new DenunciarHiloCommand(Guid.NewGuid());
            var usuarioId = Guid.NewGuid();
            _user.UsuarioId.Returns(usuarioId);
            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns(_hilo);
            _hilosRepository.HaDenunciado(_hilo.Id, Arg.Any<UsuarioId>()).Returns(false);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _unitOfWork.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
        }
    }

}