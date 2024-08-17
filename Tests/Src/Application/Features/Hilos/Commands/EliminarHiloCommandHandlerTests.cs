using Application.Hilos.Commands;
using Domain.Comentarios;
using Domain.Hilos;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Tests.Application.Hilos.Commands
{
    public class EliminarHiloCommandHandlerTests : HiloBaseCommand
    {
        private readonly EliminarHiloCommandHandler _handler;

        public EliminarHiloCommandHandlerTests()
        {
            _handler = new EliminarHiloCommandHandler(_unitOfWork, _hilosRepository, _timeProvider);
        }
        [Fact]
        public async Task Handle_Deberia_Retornar_HilosFailures_NoEncontrado_Si_Hilo_No_Existe()
        {
            // Arrange
            var command = new EliminarHiloCommand(Guid.NewGuid());
            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns((Hilo?)null);

            // Act 
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.NoEncontrado);

        }

        [Fact]
        public async Task Handle_Deberia_Retornar_Error_Si_Eliminar_Hilo_Falla()
        {
            // Arrange
            var command = new EliminarHiloCommand(Guid.NewGuid());
            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns(_hilo);

            await _hilo.Eliminar(_hilosRepository, _timeProvider.UtcNow);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.YaEliminado);

            _unitOfWork.DidNotReceive().SaveChangesAsync();
        }

        [Fact]
        public async Task Handle_Deberia_Eliminar_Hilo_Y_Guardar_Cambios_Si_Todo_Es_Valido()
        {
            // Arrange
            var command = new EliminarHiloCommand(Guid.NewGuid());
            _hilosRepository.GetHiloById(Arg.Any<HiloId>()).Returns(_hilo);
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            result.IsSuccess.Should().BeTrue();
            _unitOfWork.Received(1).SaveChangesAsync();
        }
    }
}