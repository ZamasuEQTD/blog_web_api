using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Usuarios;
using FluentAssertions;
using NSubstitute;
using NUnit.Framework;

namespace Tests.Domain.Comentarios
{
    [TestFixture]
    public class ComentarioTests
    {
        private Hilo _hilo;
        private Comentario _comentario;
        private IHilosRepository _hilosRepository;
        private IComentariosRepository _comentariosRepository;
        private DateTime _now;
        [SetUp]
        public void SetUp()
        {
            _now = DateTime.Now;

            _hilosRepository = Substitute.For<IHilosRepository>();

            _comentariosRepository = Substitute.For<IComentariosRepository>();

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

            _comentario = new Comentario(
                _hilo.Id,
                new UsuarioId(Guid.NewGuid()),
                Texto.Create("Skibidop").Value
            );

            _hilosRepository.GetDenuncias(_hilo.Id).Returns(Task.FromResult<List<DenunciaDeHilo>>([]));
        }


        [Test]
        public async Task Eliminar_Deberia_Retornar_Failure_Si_Hilo_Es_Inactivo()
        {
            await _hilo.Eliminar(_hilosRepository, _now);

            var result = _comentario.Eliminar(_hilo);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.Inactivo);
        }

        [Test]
        public void Eliminar_Deberia_Retornar_Eliminar_Comentario()
        {
            var result = _comentario.Eliminar(_hilo);

            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public async Task Denunciar_Deberia_Retornar_Failure_Si_YaHaDenunciado()
        {
            // Arrange
            var usuario = new UsuarioId(Guid.NewGuid());
            _comentariosRepository.HaDenunciado(_comentario.Id, usuario).Returns(true);

            // Act
            var result = await _comentario.Denunciar(_comentariosRepository, usuario);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.YaHaDenunciado);
        }

        [Test]
        public async Task Denunciar_Deberia_Retornar_DenunciaDeComentario_Si_No_Ha_Denunciado()
        {
            // Arrange
            var usuario = new UsuarioId(Guid.NewGuid());
            _comentariosRepository.HaDenunciado(_comentario.Id, usuario).Returns(false);

            // Act
            var result = await _comentario.Denunciar(_comentariosRepository, usuario);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.ComentarioId.Should().Be(_comentario.Id);
            result.Value.DenuncianteId.Should().Be(usuario);
            // Puedes verificar otros valores de la denuncia si es necesario
        }
    }
}
