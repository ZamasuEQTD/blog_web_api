using Domain.Usuarios;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Domain.Usuarios.ValueObject
{
    [TestFixture]
    public class UsernameTests
    {
        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Longitud_Es_Menor_Que_MIN_LENGTH()
        {
            // Arrange
            var usernameInvalido = "abc"; // Longitud menor que MIN_LENGTH

            // Act
            var result = Username.Create(usernameInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDeUsernameInvalida);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Longitud_Es_Mayor_Que_MAXIMO_LENGTH()
        {
            // Arrange
            var usernameInvalido = "abcdefghijklmnopqrstuvwxyz"; // Longitud mayor que MAXIMO_LENGTH

            // Act
            var result = Username.Create(usernameInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDeUsernameInvalida);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Contiene_Espacios_En_Blanco()
        {
            // Arrange
            var usernameInvalido = "codu bi202";

            // Act
            var result = Username.Create(usernameInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameTieneEspaciosEnBlanco);
        }

        [Test]
        public void Create_Debe_Retornar_Username_Cuando_Es_Valido()
        {
            // Arrange
            var usernameValido = "usuarioValido123";

            // Act
            var result = Username.Create(usernameValido);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Should().Be(usernameValido);
        }
    }
}