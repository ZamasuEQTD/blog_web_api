using Domain.Comentarios.Failures;
using Domain.Hilos;
using Domain.Usuarios;
using Domain.Usuarios.Failures;
using SharedKernel;

namespace Domain.Comentarios.Services.Strategies
{
    public interface IDestacadorStrategy {
        Result Destacar(Comentario comentario, Hilo hilo);
    }

    public class DestacadorStrategy : IDestacadorStrategy {
        private readonly UsuarioId _usuarioId;
        private readonly int _destacados;
        public DestacadorStrategy(UsuarioId usuarioId, int destacados)
        {
            this._usuarioId = usuarioId;
            this._destacados = destacados;
        }

        public Result Destacar(Comentario comentario, Hilo hilo)
        {
            if(!hilo.EsAutor(_usuarioId)) return UsuariosFailures.NO_ES_AUTOR;

            if(hilo.HaAlcanzadoLimiteDeDestacados(_destacados)) return new Error("Comentarios.HaAlcanzadoMaximaCantidadDeDestacados");

            return comentario.Destacar(hilo);
        }
    }
}