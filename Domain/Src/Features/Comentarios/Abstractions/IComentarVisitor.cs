using Domain.Usuarios;
using SharedKernel;

namespace Domain.Comentarios.Abstractions
{
    public interface IComentarVisitor {
        public Result<Comentario> Visit(Anonimo anonimo);
        public Result<Comentario> Visit(Moderador moderador);
    }

}