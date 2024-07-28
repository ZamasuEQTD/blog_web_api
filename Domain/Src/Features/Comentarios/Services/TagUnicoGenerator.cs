using Domain.Comentarios.ValueObjects;
using Domain.Common.Abstractions;

namespace Domain.Comentarios.Services
{
    public class TagUnicoGenerador{
        private readonly IRandomTextGenerator _random; 
        public TagUnicoGenerador(IRandomTextGenerator random)
        {
            _random = random;
        }

        public TagUnico Generar() => TagUnico.Create(_random.BuildRandomString(TagUnico.Length)).Value;
    }
 
}