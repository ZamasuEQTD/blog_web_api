using Domain.Stickies;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules {
    public class NoDebeTenerStickyActivoRule : IBusinessRule
    {
        private readonly List<Sticky> _stickies;
        private readonly DateTime _now;

        public NoDebeTenerStickyActivoRule(DateTime now, List<Sticky> stickies)
        {
            _now = now;
            _stickies = stickies;
        }

        public string Message => "Ya posee sticky";

        public bool IsBroken() => _stickies.SingleOrDefault(s=> s.Activo(_now)) is not null;
    }
}