namespace Domain.Stickies.Abstractions
{
    public interface IStickiesRepository
    {
        public void Add(Sticky sticky);
        public Task<Sticky?> GetStickyById(StickyId id);
    }    
}