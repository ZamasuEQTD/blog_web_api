using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;

namespace Tests.Domain.Comentarios.ValueObject
{
    public class TagTest
    {
        [Fact]
        public void Create_Debe_RetornarFailureResult_Cuando_EsVacio()
        {
            var result = Tag.Create("");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(TagsFailures.TAG_INVALIDO);
        }
        [Fact]
        public void Create_Debe_RetornarFailureResult_Cuando_NoEsMayuscula()
        {
            var result = Tag.Create("FKMFASDs");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(TagsFailures.TAG_INVALIDO);
        }
        [Fact]
        public void Create_Debe_RetornarFailureResult_Cuando_SuperaLargo()
        {
            var result = Tag.Create("FKMFASDSDSD");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(TagsFailures.TAG_INVALIDO);
        }
        [Fact]
        public void Create_Debe_RetornarSuccessResult_Cuando_Valido()
        {
            var result = Tag.Create("FKMFASDS");

            result.IsSuccess.Should().BeTrue();
        }
    }
}