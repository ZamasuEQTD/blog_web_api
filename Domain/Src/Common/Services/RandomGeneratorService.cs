namespace Domain.Common.Abstractions {
    public class RandomGeneratorService : IRandomGeneratorService
    {
        static private readonly Random random = new Random();

        public int GenerarInt(int min, int max)
        {
            return random.Next(min, max);
        }

        public int GenerarInt(int max)
        {
            return GenerarInt(0,max);
        }

        public int GenerarSeedInt(int seed, int max)
        {
            return GenerarSeedInt(seed,0,max);
        }

        public int GenerarSeedInt(int seed, int min, int max)
        {
            Random random = new Random(seed);
            
            return random.Next(min, max);
        }
    }
}