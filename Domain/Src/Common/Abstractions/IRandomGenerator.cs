namespace Domain.Common.Abstractions
{
    public interface IRandomGeneratorService {
        int GenerarInt(int min, int max);
        int GenerarInt(int max);
    }

    public class SeededRandomGenerator : IRandomGeneratorService {
        private readonly Random _random;
        public SeededRandomGenerator(int seed) {
            _random = new Random(seed);
        }

        public int GenerarInt(int min, int max)=> _random.Next(min, max);
        public int GenerarInt(int max) => _random.Next(max);
    }
    
    public class RandomGenerator : IRandomGeneratorService {
        static private readonly Random _random = new();
        public int GenerarInt(int min, int max) => _random.Next(min,max);
        public int GenerarInt(int max)=> _random.Next(max);
    }

}