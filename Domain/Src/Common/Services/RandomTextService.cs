using System.Text;
using Domain.Common.Abstractions;

namespace Domain.Comentarios.Services
{
    public class RandomTextService : IRandomTextService
    {
        private readonly IRandomGeneratorService _randomGeneratorService;

        public RandomTextService(IRandomGeneratorService randomGeneratorService)
        {
            _randomGeneratorService = randomGeneratorService;
        }

        public string BuildRandomSeedString(int seed, int length, List<char> caracteres)
        {
            return BuildString(
                length,
                caracteres,
                (i)=> _randomGeneratorService.GenerarSeedInt(seed + i,caracteres.Count)
            );
        }

        public string BuildRandomString(int length, List<char> caracteres)
        {
           return BuildString(
                length,
                caracteres,
                (i)=> _randomGeneratorService.GenerarInt(caracteres.Count)
            );
        }

        private static string BuildString(int length, List<char> caracteres, Func<int, int> calcularIndice){
            StringBuilder builder = new();

            for (int i = length - 1; i >= 0 ; i--)
            {
                int randomIndex = calcularIndice(i);

                builder.Append(caracteres[randomIndex]);
            }
            return builder.ToString();
        }
    }
}