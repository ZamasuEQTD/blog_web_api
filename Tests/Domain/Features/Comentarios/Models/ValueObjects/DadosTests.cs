using Domain.Comentarios;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;
using SharedKernel;
using Xunit;

namespace Tests.Domain.Comentarios.ValueObjects
{
    public class DadosTests
    {
        [Fact]
        public void Create_DebeRetornar_FailureResult_CuandoDadosEsMenorAlMinimo()
        {
            Result<Dados> result = Dados.Create(0);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.ValorDeDadosInvalidos);
        }
        [Fact]
        public void Create_DebeRetornar_FailureResult_CuandoDadosEsMayorAlMaximo()
        {
            Result<Dados> result = Dados.Create(10);

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.ValorDeDadosInvalidos);
        }
        [Fact]
        public void Create_Debe_RetornaDados()
        {
            Dados dados = Dados.Create(2).Value;

            Result<Dados> result = Dados.Create(2);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().Be(dados);
        }
    }
}