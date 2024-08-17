using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Domain.Comentarios
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Es_Vacio()
        {
            // Arrange
            var tagInvalido = string.Empty;

            // Act
            var result = Tag.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Tiene_Longitud_Incorrecta()
        {
            // Arrange
            var tagInvalido = "TAGDEMASIADOLARGO"; // Longitud mayor que Tag.LENGTH

            // Act
            var result = Tag.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Contiene_Caracteres_Invalidos()
        {
            // Arrange
            var tagInvalido = "TAG-1234"; // Contiene un guión, que no está permitido

            // Act
            var result = Tag.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Tag_Contiene_Minusculas()
        {
            // Arrange
            var tagInvalido = "tag12345"; // Contiene minúsculas

            // Act
            var result = Tag.Create(tagInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }

        [Test]
        public void Create_Debe_Retornar_Tag_Cuando_Es_Valido()
        {
            // Arrange
            var tagValido = "TAG12345";

            // Act
            var result = Tag.Create(tagValido);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Should().Be(tagValido);
        }
    }
}