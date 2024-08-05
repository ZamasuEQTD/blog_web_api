using Domain.Comentarios;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules
{
    
    public class ComentarioDebeExistirRule : IBusinessRule {

        private readonly List<Comentario> _comentarios;
        private readonly ComentarioId _comentarioId;

        public ComentarioDebeExistirRule(ComentarioId comentarioId, List<Comentario> comentarios)
        {
            _comentarioId = comentarioId;
            _comentarios = comentarios;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => _comentarios.FirstOrDefault(c=> c.Id == _comentarioId) is null;
    }
}