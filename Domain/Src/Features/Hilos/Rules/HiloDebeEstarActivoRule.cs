using SharedKernel.Abstractions;

namespace Domain.Hilos.Rules {
    public class HiloDebeEstarActivoRule : IBusinessRule {
        private readonly Hilo.HiloStatus _status;
        public HiloDebeEstarActivoRule(Hilo.HiloStatus status)
        {
            _status = status;
        }

        public string Message => "Hilo inactivo";

        public bool IsBroken()=> _status != Hilo.HiloStatus.Activo;
    }
}