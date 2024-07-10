namespace Domain.Common.Abstractions
{
    public interface IRandomGeneratorService {
        int GenerarInt(int min, int max);
        int GenerarInt(int max);
        int GenerarSeedInt(int seed,int max);
        int GenerarSeedInt(int seed,int min, int max);
    }
}