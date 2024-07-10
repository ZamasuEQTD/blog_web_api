using Domain.Comentarios.Services;
using Domain.Comentarios.ValueObjects;
using FluentAssertions;
namespace Tests.Domain.Comentarios.Services
{
    public class TagUtilsTest
    {
        [Fact]
        public void CantidadDeTags_Debe_RetornarDos_Cuando_Hay_DosTaggueos()
        {
            int cantidad = TagUtils.CantidadDeTags(">>FDSDSXAD>>FAFFFDSC");
        
            cantidad.Should().Be(2);
        }
        [Fact]
        public void CantidadDeTags_Debe_RetornarCero_Cuando_NoHay_Taggueos()
        {
            int cantidad = TagUtils.CantidadDeTags("FAFFFDSC");
        
            cantidad.Should().Be(0);
        }

        [Fact]
        public void GetTags_Debe_RetornarDosTaggs_Cuando_Hay_Dos_Taggueos() {
            HashSet<Tag> assert = [
                Tag.Create("FDSDSXAD").Value,
                Tag.Create("FAFFFDSC").Value
            ];
            
            List<Tag> tags = TagUtils.GetTags(">>FDSDSXAD>>FAFFFDSC");
            
            foreach (var tag in tags)
            {
                assert.Contains(tag).Should().BeTrue();
            }
        }
        [Fact]
        public void GetTags_Debe_RetornarVacio_Cuando_No_Hay_Taggueos() {
            
            List<Tag> tags = TagUtils.GetTags("FAFFFDSC");

            tags.Should().BeEmpty();
        }

        [Fact]
        public void GetTagsUnicos_Debe_RetornarVacio_Cuando_No_Hay_Taggueos() {
            HashSet<Tag> tags = TagUtils.GetTagsUnicos("FAFFFDSC");

            tags.Should().BeEmpty();
        }


        [Fact]
        public void GetTagsUnicos_Debe_RetornarDosTags_Cuando_Hay_Dos_Taggueos() {
            HashSet<Tag> assert = [
                Tag.Create("FDSDSXAD").Value,
                Tag.Create("FAFFFDSC").Value
            ];
            
            HashSet<Tag> tags = TagUtils.GetTagsUnicos(">>FDSDSXAD>>FAFFFDSC");
            
            foreach (var tag in tags)
            {
                assert.Contains(tag).Should().BeTrue();
            }
        }
    }
}