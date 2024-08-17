using Domain.Categorias;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Stickies;
using Domain.Usuarios;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Domain.Hilos
{
    [TestFixture]
    public class HiloTests
    {
        private UsuarioId _usuario = new UsuarioId(Guid.NewGuid());
        private Hilo _hilo;
        private Sticky _sticky;
        private IHilosRepository _hilosRepository;
        private DateTime now;
        public HiloTests()
        {
            now = DateTime.UtcNow;

            _hilosRepository = Substitute.For<IHilosRepository>();
        }

        [SetUp]
        public void SetUp()
        {
            _hilo = new Hilo(
                Titulo.Create("A que huele?").Value,
                Descripcion.Create("Eso.Debatan").Value,
                new UsuarioId(Guid.NewGuid()),
                new SubcategoriaId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(
                    true,
                    true
                )
            );

            _sticky = new Sticky(
                _hilo.Id,
                null
            );
            _hilosRepository.GetDenuncias(_hilo.Id).Returns(Task.FromResult<List<DenunciaDeHilo>>([]));
        }

        [Test]
        public async Task Eliminar_Debe_Retornar_Failure_Cuando_Hilo_YaEsta_Eliminado()
        {
            await _hilo.Eliminar(
                _hilosRepository,
                now
            );

            var result = await _hilo.Eliminar(
                _hilosRepository,
                now
            );

            result.Error.Should().Be(HilosFailures.YaEliminado);
        }

        [Test]
        public async Task Eliminar_Debe_Eliminar_StickyActivo()
        {
            _hilosRepository.GetStickyActivo(_hilo.Id, now).Returns(Task.FromResult<Sticky?>(_sticky));

            await _hilo.Eliminar(
                _hilosRepository,
                now
            );

            _sticky.Activo(now).Should().BeFalse();
        }
        [Test]
        public async Task Eliminar_Debe_Retornar_Success_Cuando_Hilo_NoEsta_Eliminado()
        {
            var result = await _hilo.Eliminar(
                _hilosRepository,
                now
            );

            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task EstablecerSticky_Debe_Retornar_Failure_Cuando_YaTieneStickyActivo()
        {
            // Arrange
            _hilosRepository.TieneStickyActivo(_hilo.Id, now).Returns(true);

            // Act
            var result = await _hilo.EstablecerSticky(_hilosRepository, now, null); // O cualquier fecha de conclusión válida

            // Assert
            result.Error.Should().Be(HilosFailures.YaTieneStickyActivo);
        }

        [Test]
        public async Task EstablecerSticky_Debe_Retornar_Sticky_Cuando_NoTieneStickyActivo()
        {
            // Arrange
            _hilosRepository.TieneStickyActivo(_hilo.Id, now).Returns(false); // Configurar el mock para simular que NO tiene un sticky activo
            var concluye = now.AddDays(1); // O cualquier fecha de conclusión válida

            // Act
            var result = await _hilo.EstablecerSticky(_hilosRepository, now, concluye);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Hilo.Should().Be(_hilo.Id);
            result.Value.Conluye.Should().Be(concluye);
        }

        [Test]
        public async Task Denunciar_Debe_Retornar_Failure_Cuando_YaHaDenunciado()
        {
            // Arrange
            _hilosRepository.HaDenunciado(_hilo.Id, _usuario).Returns(true); // Configurar el mock para simular que el usuario ya ha denunciado

            // Act
            var result = await _hilo.Denunciar(_hilosRepository, _usuario);

            // Assert
            result.Error.Should().Be(HilosFailures.YaHaDenunciado);
        }

        [Test]
        public async Task Denunciar_Debe_Retornar_DenunciaDeHilo_Cuando_NoHaDenunciado()
        {
            // Arrange
            _hilosRepository.HaDenunciado(_hilo.Id, _usuario).Returns(false); // Configurar el mock para simular que el usuario NO ha denunciado

            // Act
            var result = await _hilo.Denunciar(_hilosRepository, _usuario);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.HiloId.Should().Be(_hilo.Id);
            result.Value.DenuncianteId.Should().Be(_usuario);
        }
    }

}