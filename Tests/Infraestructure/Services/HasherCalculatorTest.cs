using Application.Medias.Abstractions;
using FluentAssertions;
using Infraestructure.Media;
using Xunit;

namespace Tests.Infraestructure
{

    public class HasherTest
    {

        private readonly IHasher _hasher = new Hasher();

        [Fact]
        public async Task Hash_String_Devuelve_El_mismo_Hash()
        {
            var hash = await _hasher.Hash("hola");

            var res = await _hasher.Hash("hola");

            res.Should().Be(hash);
        }

        [Fact]
        public async Task Hash_Stream_Devuelve_El_mismo_Hash()
        {
            var hash = await _hasher.Hash(new MemoryStream([214, 2, 8, 12]));

            var res = await _hasher.Hash(new MemoryStream([214, 2, 8, 12]));

            res.Should().Be(hash);
        }
    }
}