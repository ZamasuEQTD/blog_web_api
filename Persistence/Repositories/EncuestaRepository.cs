using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Usuarios;

namespace Persistence.Repositories
{
    public class EncuestaRepository : IEncuestasRepository
    {
        public Task<bool> ExisteRespuesta(EncuestaId id, RespuestaId respuesta)
        {
            throw new NotImplementedException();
        }

        public Task<Encuesta> GetEncuestaById(EncuestaId id)
        {
            throw new NotImplementedException();
        }

        public Task<Respuesta?> GetRespuesta(RespuestaId id)
        {
            throw new NotImplementedException();
        }

        public Task<bool> HaVotado(EncuestaId id, UsuarioId usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}