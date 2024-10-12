using Domain.Categorias;
using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
using Domain.Hilos.ValueObjects;
using Domain.Usuarios;
using Xunit;

namespace Tests.Domain.Comentarios
{
    public class ComentarioTests
    {
        private readonly Comentario _comentario;

        public ComentarioTests()
        {
            _comentario = new Comentario(
                new HiloId(Guid.NewGuid()),
                new UsuarioId(Guid.NewGuid()),
                null,
                Texto.Create("una tipa rapaz").Value,
                Colores.Amarillo,
                new InformacionDeComentario(
                    Tag.Create("12345678").Value,
                    null,
                    null
                )
            );
        }

        [Fact]
        public void TestName()
        {
            Hilo hilo = new Hilo(
                Titulo.Create("TituloDeHilo").Value,
                Descripcion.Create("").Value,
                new UsuarioId(Guid.NewGuid()),
                new(Guid.NewGuid()),
                new SubcategoriaId(Guid.NewGuid()),
                null,
                new ConfiguracionDeComentarios(false, false)
            );

            hilo.Eliminar(DateTime.Now);

            var result = _comentario.Denunciar(hilo, new UsuarioId(Guid.NewGuid()));

            // Given

            // When

            // Then
        }
    }
}