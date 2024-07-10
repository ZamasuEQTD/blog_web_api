using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;

namespace Tests.Domain.Comentarios.ValueObject
{
    public class DadosTest
    {
        [Fact]
        public void Create_Debe_RetornarFailureResult_Cuando_DadosEsMenorAlMinimo()
        {
            var result = Dados.Create(0);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DadosFailures.VALOR_INVALIDO);
        }
        [Fact]
        public void Create_Debe_RetornarFailureResult_Cuando_DadosEsMayorAlMaximo()
        {
            var result = Dados.Create(7);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(DadosFailures.VALOR_INVALIDO);
        }
        [Fact]
        public void Create_Debe_RetornarSuccessResult_Cuando_DadosEsValido()
        {
            var result = Dados.Create(3);

            result.IsSuccess.Should().BeTrue();
            result.Value.Value.Should().Be(3);
        }
    }
}