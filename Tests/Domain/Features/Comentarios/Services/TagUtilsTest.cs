using FluentAssertions;
using NUnit.Framework;
using Domain.Comentarios.ValueObjects;
namespace Domain.Comentarios.Services
{
    [TestFixture]
    public class TagUtilsTest
    {
        [Test]
        public void CantidadDeTags_Debe_RetornarDos_Cuando_Hay_DosTaggueos()
        {
            int cantidad = TagUtils.CantidadDeTags(">>FDSDSXAD>>FAFFFDSC");

            cantidad.Should().Be(2);
        }
        [Test]
        public void CantidadDeTags_Debe_RetornarCero_Cuando_NoHay_Taggueos()
        {
            int cantidad = TagUtils.CantidadDeTags("FAFFFDSC");

            cantidad.Should().Be(0);
        }

        [Test]
        public void GetTags_Debe_RetornarDosTaggs_Cuando_Hay_Dos_Taggueos()
        {
            HashSet<string> assert = [
                "FDSDSXAD",
                "FAFFFDSC"
            ];

            List<string> tags = TagUtils.GetTags(">>FDSDSXAD>>FAFFFDSC");

            foreach (var tag in tags)
            {
                assert.Contains(tag).Should().BeTrue();
            }
        }
        [Test]
        public void GetTags_Debe_RetornarVacio_Cuando_No_Hay_Taggueos()
        {

            List<string> tags = TagUtils.GetTags("FAFFFDSC");

            tags.Should().BeEmpty();
        }

        [Test]
        public void GetTagsUnicos_Debe_RetornarVacio_Cuando_No_Hay_Taggueos()
        {
            HashSet<string> tags = TagUtils.GetTagsUnicos("FAFFFDSC");

            tags.Should().BeEmpty();
        }


        [Test]
        public void GetTagsUnicos_Debe_RetornarDosTags_Cuando_Hay_Dos_Taggueos()
        {
            HashSet<string> assert = [
                "FDSDSXAD",
                "FAFFFDSC"
            ];

            HashSet<string> tags = TagUtils.GetTagsUnicos(">>FDSDSXAD>>FAFFFDSC");

            foreach (var tag in tags)
            {
                assert.Contains(tag).Should().BeTrue();
            }
        }
    }
}