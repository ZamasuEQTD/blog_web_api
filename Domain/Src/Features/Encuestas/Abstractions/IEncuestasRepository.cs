namespace Domain.Encuestas.Abstractions
{
    public interface IEncuestasRepository
    {
        Task<Encuesta> GetEncuestaById(EncuestaId id);
    }
}