namespace Domain.Hilos.Abstractions
{
    public interface IHilosRepository
    {
        public void Add(Hilo hilo);
        public Task<Hilo?> GetHiloById(HiloId id);
    }
}