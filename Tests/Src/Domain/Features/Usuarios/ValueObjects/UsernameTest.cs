using Domain.Usuarios;
using Domain.Usuarios.Failures;
using FluentAssertions;

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
            result.Error.Should().Be(UsernameFailures.TIENE_ESPACIOS_EN_BLANCO);
        }
        [Fact]
        public void Create_DebeRetornarResultFailure_CuandoEstaVacio()
        {
            string txt = "";

            var result = Username.Create(txt);
        
            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(UsernameFailures.LARGO_INVALIDO);
        }
    }
}