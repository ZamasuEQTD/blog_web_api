namespace Domain.Encuestas.Abstractions
{
    public interface IEncuestasRepository
    {
        public void Add(Encuesta encuesta);
        public Task<Encuesta?> GetEncuestaById(EncuestaId id);
    }
}