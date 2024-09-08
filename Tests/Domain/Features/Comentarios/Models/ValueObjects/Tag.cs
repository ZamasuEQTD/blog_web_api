using Domain.Comentarios.ValueObjects;
using FluentAssertions;
using NUnit.Framework;
using SharedKernel;

namespace Domain.Comentarios.Models.ValueObjects
{
    [TestFixture]
    public class TagTests
    {
        [Test]
        public void Create_DebeRetornar_FailureResult_CuandoTagEstaVacio()
        {
            Result<Tag> result = Tag.Create("");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }

        [Test]
        public void Create_DebeRetornar_FailureResult_CuandoTagTieneLongitudMayor()
        {
            Result<Tag> result = Tag.Create("DADSDASDA");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }
        [Test]
        public void Create_DebeRetornar_FailureResult_CuandoTagTieneLongitudMenor()
        {
            Result<Tag> result = Tag.Create("DA");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }
        [Test]
        public void Create_DebeRetornar_FailureResult_CuandoTagTieneEsMiniscula()
        {
            Result<Tag> result = Tag.Create("dsadadsx");

            result.IsFailure.Should().BeTrue();
            result.Error.Should().Be(ComentariosFailures.TagInvalido);
        }
        [Test]
        public void Create_DebeRetornar_SuccessResult_CuandoEsTagCompuestPorMayusculas()
        {
            Result<Tag> result = Tag.Create("XXXXXXXX");

            result.IsSuccess.Should().BeTrue();
        }

        [Test]
        public void Create_DebeRetornar_SuccessResult_CuandoEsTagCompuestPorMayusculasNumeros()
        {
            Result<Tag> result = Tag.Create("4XXXXX22");

            result.IsSuccess.Should().BeTrue();
        }
        [Test]
        public void Create_DebeRetornar_SuccessResult_CuandoEsTagCompuestPorNumeros()
        {
            Result<Tag> result = Tag.Create("12345678");

            result.IsSuccess.Should().BeTrue();
        }
    }
}