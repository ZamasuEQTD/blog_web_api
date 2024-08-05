using Domain.Stickies;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules
{
    
    public class DebeTenerStickyActivoRule : IBusinessRule
    {
        private readonly List<Sticky> _stickies;
        private readonly DateTime _now;

        public DebeTenerStickyActivoRule(DateTime now, List<Sticky> stickies)
        {
            _now = now;
            _stickies = stickies;
        }

        public string Message => "No hay stickie activo";

        public bool IsBroken() => _stickies.SingleOrDefault(s=> s.Activo(_now)) is null;
    }
}