using Domain.Usuarios;
using Domain.Usuarios.ValueObjects;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Domain.Usuarios.ValueObject
{
    [TestFixture]
    public class PasswordTests
    {
        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Longitud_Es_Menor_Que_MIN_LENGTH()
        {
            // Arrange
            var passwordInvalido = "1234567"; // Longitud menor que MIN_LENGTH

            // Act
            var result = Password.Create(passwordInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDePasswordInvalida);
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Longitud_Es_Mayor_Que_MAXIMO_LENGTH()
        {
            // Arrange
            var passwordInvalido = "unpasswordmuylargoparaservalido"; // Longitud mayor que MAXIMO_LENGTH

            // Act
            var result = Password.Create(passwordInvalido);

            // Assert
            result.Error.Should().Be(UsuariosFailures.LongitudDePasswordInvalida);
            result.IsFailure.Should().BeTrue();
        }

        [Test]
        public void Create_Debe_Retornar_Failure_Cuando_Contiene_Espacios_En_Blanco()
        {
            // Arrange
            var passwordInvalido = "password  ";

            // Act
            var result = Password.Create(passwordInvalido);

            // Assert
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.PasswordTieneEspaciosEnBlanco);
        }

        [Test]
        public void Create_Debe_Retornar_Password_Cuando_Es_Valido()
        {
            // Arrange
            var passwordValido = "passwordValida";

            // Act
            var result = Password.Create(passwordValido);

            // Assert
            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Should().Be(passwordValido);
        }
    }
}