using Domain.Usuarios;

namespace Domain.Encuestas.Abstractions
{
    public interface IEncuestasRepository
    {
        Task<Encuesta> GetEncuestaById(EncuestaId id);
        void Add(Encuesta encuesta);
    }
}