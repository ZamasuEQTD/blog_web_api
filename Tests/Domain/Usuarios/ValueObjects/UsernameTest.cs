using Domain.Usuarios;
using FluentAssertions;
using Xunit;

namespace Tests.Domain.Usuarios.ValueObject
{
    public class UsernameTest
    {

        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoHayEspaciosEnBlanco()
        {
            string txt = "texto     ";

            var result = Username.Create(txt);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.UsernameTieneEspaciosEnBlanco);
        }
        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoEstaVacio()
        {
            string txt = "";

            var result = Username.Create(txt);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsuariosFailures.LongitudDeUsernameInvalida);
        }
    }
}