using Domain.Comentarios.ValueObjects;
using Domain.Common.Abstractions;

namespace Domain.Comentarios.Services
{
    public class TagGenerator {
        private readonly IRandomTextGenerator _random;

        public TagGenerator(IRandomTextGenerator random)
        {
            _random = random;
        }

        public Tag Generar() => Tag.Create(_random.BuildRandomString(8)).Value;
    }
}