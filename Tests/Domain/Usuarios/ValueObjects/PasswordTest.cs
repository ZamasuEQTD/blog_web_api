using Domain.Usuarios;
using Domain.Usuarios.ValueObjects;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Usuarios.ValueObject
{
    public class PasswordTest
    {
        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoLongitudEsMenorAlMinimo()
        {
            string password = "corta"; // Longitud menor que MIN_LENGTH

            var result = Password.Create(password);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDePasswordInvalida);
        }

        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoLongitudEsMayorAlMaximo()
        {
            string password = "estacontraseñaesdemasiadolarga"; // Longitud mayor que MAXIMO_LENGTH

            var result = Password.Create(password);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDePasswordInvalida);
        }

        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoContieneEspaciosEnBlanco()
        {
            string password = "contraseña con";

            var result = Password.Create(password);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.PasswordTieneEspaciosEnBlanco);
        }

        [Fact]
        public void Create_DebeRetornarPassword_CuandoEsValida()
        {
            string password = "ValidPassw123";

            var result = Password.Create(password);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().NotBeNull();
            result.Value.Value.Should().Be(password);
        }
    }
}