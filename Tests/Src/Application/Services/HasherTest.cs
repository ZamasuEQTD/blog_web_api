using Application.Common.Services;
using FluentAssertions;
using NUnit.Framework;

namespace Tests.Application
{
    [TestFixture]
    public class HasherTest
    {
        [Test]
        public async Task Hash_String_Devuelve_El_mismo_Hash()
        {
            var hash = await Hasher.Hash("hola");

            var res = await Hasher.Hash("hola");

            res.Should().Be(hash);
        }

        [Test]
        public async Task Hash_Stream_Devuelve_El_mismo_Hash()
        {
            var hash = await Hasher.Hash(new MemoryStream([214, 2, 8, 12]));

            var res = await Hasher.Hash(new MemoryStream([214, 2, 8, 12]));

            res.Should().Be(hash);
        }
    }
}