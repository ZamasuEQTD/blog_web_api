using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Domain.Comentarios
{
    [TestFixture]
    public class TagUnicoTests
    {
        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Es_Vacio()
        {
            // Arrange
            var tagInvalido = string.Empty;

            // Act
            var result = TagUnico.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagUnicoInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Tiene_Longitud_Incorrecta()
        {
            // Arrange
            var tagInvalido = "TAGDEMASIADOLARGO"; // Longitud mayor que TagUnico.Length

            // Act
            var result = TagUnico.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagUnicoInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Contiene_Caracteres_Invalidos()
        {
            // Arrange
            var tagInvalido = "TA-G"; // Contiene un guión, que no está permitido

            // Act
            var result = TagUnico.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagUnicoInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Contiene_Minusculas()
        {
            // Arrange
            var tagInvalido = "taG"; // Contiene minúsculas

            // Act
            var result = TagUnico.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagUnicoInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_TagUnico_Cuando_Es_Valido()
        {
            // Arrange
            var tagValido = "TAG";

            // Act
            var result = TagUnico.Create(tagValido);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Should().Be(tagValido);
        }
    }
}