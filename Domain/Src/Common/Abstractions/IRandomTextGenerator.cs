namespace Domain.Common.Abstractions {
    public interface IRandomTextService {
        string BuildRandomString(int length, List<char> caracteres);
        string BuildRandomSeedString(int seed,int length, List<char> caracteres);
    }
}