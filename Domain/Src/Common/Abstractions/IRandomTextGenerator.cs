using System.Text;
using Domain.Comentarios.ValueObjects;

namespace Domain.Common.Abstractions {
    public interface IRandomTextGenerator {
        public string BuildRandomString(int length);
    }
    
    public class RandomTextGenerator : IRandomTextGenerator {
        private static readonly string _caracteres_string = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789"; 
        public static  readonly List<char> caracteres = _caracteres_string.ToList();
        private readonly IRandomGeneratorService _random;
        public RandomTextGenerator(IRandomGeneratorService random)
        {
            _random = random;
        }

        public string BuildRandomString(int length) {
            StringBuilder builder = new();

            for (int i = length - 1; i >= 0 ; i--) {
                int randomIndex = _random.GenerarInt(caracteres.Count);

                builder.Append(caracteres[randomIndex]);
            }
            return builder.ToString();
        }
    }

    
}