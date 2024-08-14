using Domain.Usuarios;

namespace Domain.Encuestas.Abstractions
{
    public interface IEncuestasRepository
    {
        Task<Encuesta> GetEncuestaById(EncuestaId id);
        Task<bool> HaVotado(EncuestaId id, UsuarioId usuarioId);
        Task<bool> ExisteRespuesta(EncuestaId id, RespuestaId respuesta);
        Task<Respuesta?> GetRespuesta(RespuestaId id);

    }
}