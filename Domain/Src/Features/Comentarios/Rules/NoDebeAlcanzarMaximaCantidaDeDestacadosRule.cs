using Domain.Hilos;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Rules
{
    public class NoDebeAlcanzarMaximaCantidadDeDestacadosRule : IBusinessRule {
        private readonly List<Comentario> _comentarios;

        public NoDebeAlcanzarMaximaCantidadDeDestacadosRule(List<Comentario> comentarios)
        {
            _comentarios = comentarios;
        }

        public string Message => "No puedes destacar más comentarios";
        public bool IsBroken() => _comentarios.Count(c=> c.Destacado) == Hilo.CANTIDAD_MAXIMA_DE_DESTACADOS;
    }
}