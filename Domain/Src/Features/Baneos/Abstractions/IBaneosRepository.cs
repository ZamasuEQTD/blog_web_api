namespace Domain.Baneos.Abstractions
{
    public interface IBaneosRepository
    {
        void Add(Baneo baneo);
        Task<Baneo> GetBaneoById(BaneoId id);

    }
}