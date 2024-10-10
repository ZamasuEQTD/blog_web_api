using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.ValueObjects;
using Domain.Media;
using Domain.Usuarios;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Hilos
{
    public class HiloTests
    {
        private readonly Hilo _hilo;
        private DateTime _now => DateTime.Now;
        public HiloTests()
        {
            _hilo = new Hilo(
                Titulo.Create("TituloDeHilo").Value,
                Descripcion.Create("fsafafasfafas").Value,
                new UsuarioId(Guid.NewGuid()),
                new MediaReferenceId(Guid.NewGuid()),
                new SubcategoriaId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );
        }

        [Fact]
        public void Eliminar_Debe_RetornarFailureResult_CuandoYaEstaEliminado()
        {
            _hilo.Eliminar(DateTime.Now);

            var result = _hilo.Eliminar(DateTime.Now);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.YaEliminado);
        }

        [Fact]
        public void Eliminar_Debe_RetornarSuccessResult_CuandoNoEstaEliminado()
        {
            var result = _hilo.Eliminar(DateTime.Now);

            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Eliminar_Debe_DesestimarTodasLasDenuncias()
        {
            _hilo.Denunciar(
                new UsuarioId(Guid.NewGuid())
            );

            var result = _hilo.Eliminar(DateTime.Now);

            result.IsSuccess.Should().BeTrue();
            foreach (var d in _hilo.Denuncias)
            {
                d.Desestimada.Should().BeTrue();
            }
        }

        [Fact]
        public void Eliminar_Debe_EliminarStickyActivo()
        {
            _hilo.EstablecerSticky(
                DateTime.Now.AddDays(2),
                null
            );

            var result = _hilo.Eliminar(DateTime.Now);

            result.IsSuccess.Should().BeTrue();
            _hilo.TieneStickyActivo(DateTime.Now).Should().BeFalse();
        }

        [Fact]
        public void EstablecerSticky_Debe_RetornarFailureResult_CuandoYaHaySticky()
        {
            _hilo.EstablecerSticky(
                DateTime.Now.AddDays(2)
            );

            var result = _hilo.EstablecerSticky(
                DateTime.Now.AddDays(2)
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.YaTieneStickyActivo);
        }
        [Fact]
        public void EstablecerSticky_DebeRetornarSuccessResult_CuandoNoHayStickyActivo()
        {
            var result = _hilo.EstablecerSticky(
                DateTime.Now.AddDays(2),
                null
            );

            result.IsSuccess.Should().BeTrue();
            _hilo.TieneStickyActivo(DateTime.Now).Should().BeTrue();
        }

        [Fact]
        public void EliminarSticky_DebeRetornarFailurResult_CuandoNoHayStickyActivo()
        {
            var result = _hilo.EliminarSticky(_now);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.SinStickyActivo);
        }

        [Fact]
        public void EliminarSticky_DebeRetornarSuccesResult_CuandoHayStickyActivo()
        {
            _hilo.EstablecerSticky(
                _now
            );

            var result = _hilo.EliminarSticky(_now);
            result.IsSuccess.Should().BeTrue();
        }

        [Fact]
        public void Denunciar_DebeRetornarSuccessResult_CuandoNoHaDenunciado()
        {
            UsuarioId usuarioId = new UsuarioId(Guid.NewGuid());
            var result = _hilo.Denunciar(
                usuarioId
            );

            result.IsSuccess.Should().BeTrue();
            _hilo.HaDenunciado(usuarioId).Should().BeTrue();
        }

        [Fact]
        public void Denunciar_DebeRetornarFailureResult_CuandoYaHaDenunciado()
        {
            UsuarioId usuarioId = new UsuarioId(Guid.NewGuid());
            _hilo.Denunciar(usuarioId);

            var result = _hilo.Denunciar(
                usuarioId
            );

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(HilosFailures.YaHaDenunciado);
        }

        [Fact]
        public void Destacar_DebeRetornarFailureResult_CuandoNoEsAutor()
        {
            // Comentario comentario = _hilo.Comentar(
            //     Texto.Create("Este es un comentario").Value,
            //     new UsuarioId(Guid.NewGuid())
            // ).Value;

            // var result = _hilo.Destacar(new UsuarioId(Guid.NewGuid()), comentario);

            // result.IsFailure.Should().BeTrue();
            // result.Error.Should().Be(HilosFailures.NoEsAutor);
        }

        [Fact]
        public void Destacar_DebeRetornarFailureResult_CuandoComentarioEstaEliminado()
        {
            // Comentario comentario = _hilo.Comentar(
            //     Texto.Create("Este es un comentario").Value,
            //     new UsuarioId(Guid.NewGuid())
            // ).Value;

            // _hilo.Eliminar(comentario);

            // var result = _hilo.Destacar(_hilo.AutorId, comentario);

            // result.IsFailure.Should().BeTrue();
            // result.Error.Should().Be(ComentariosFailures.Inactivo);
        }

        [Fact]
        public void Destacar_DebeDestacarElComentario_CuandoNoEstaDestacado()
        {
            // Comentario comentario = _hilo.Comentar(
            //     Texto.Create("Este es un comentario").Value,
            //     new UsuarioId(Guid.NewGuid())
            // ).Value;

            // var result = _hilo.Destacar(_hilo.AutorId, comentario);

            // result.IsSuccess.Should().BeTrue();
        }
    }
}