using Domain.Medias.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Domain.Medias
{
    [TestFixture]
    public class YoutubeUtilsTests
    {
        [Test]
        public void EsYoutubeVideo_Devuelve_True_CuandoEsVideo(){
            bool result = YoutubeUtils.EsYoutubeVideo("https://www.youtube.com/watch?v=HWjCStB6k4o");
       
            result.Should().BeTrue();
        }

        [Test]
        public void EsYoutubeVideo_Devuelve_True_CuandoEsShort(){
            bool result = YoutubeUtils.EsYoutubeVideo("https://www.youtube.com/shorts/Zla1oA-8BEE");
       
            result.Should().BeTrue();
        }

        [Test]
        public void EsYoutubeVideo_Devuelve_False_CuandoNoEsLinkDeYoutube(){
            bool result = YoutubeUtils.EsYoutubeVideo("www.google.com");
       
            result.Should().BeFalse();
        }
        [Test]
        public void EsYoutubeVideo_Devuelve_False_CuandoNoEsVideoNiShort(){
            bool result = YoutubeUtils.EsYoutubeVideo("https://www.youtube.com");
       
            result.Should().BeFalse();
        }
        [Test]
        public void EsYoutubeVideo_Devuelve_False_CuandoEsCanalDeUsuario(){
            bool result = YoutubeUtils.EsYoutubeVideo("https://www.youtube.com/@MilanJovanovicTech");
       
            result.Should().BeFalse();
        }
        [Test]
        public void GetVideoId_Devuelve_VideoId_CuandoEsVideo(){
            var id = "uxs_HYw_mLk";

            string result = YoutubeUtils.GetVideoId($"https://www.youtube.com/watch?v={id}");

            result.Should().Be(id);       
        }

        [Test]
        public void GetVideoId_Devuelve_ShortId_CuandoEsShort(){
            var id = "UTLqBMWzPAI";

            string result = YoutubeUtils.GetVideoId($"https://www.youtube.com/shorts/UTLqBMWzPAI");

            result.Should().Be(id);       
        }

        [Test]
        public void GetVideoId_Lanza_Excepcion_Cuando_NoEsLinkValido(){
            
            Action result = ()=> YoutubeUtils.GetVideoId($"https://www.youtube.com");

            result.Should().Throw<Exception>();       
        }
    }
}