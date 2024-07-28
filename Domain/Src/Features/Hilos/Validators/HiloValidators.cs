using Domain.Hilos.Failures;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos.Validators
{
    public class HiloDebeEstarActivoValidator : ValidationHandler{
        private readonly Hilo _hilo;
        public HiloDebeEstarActivoValidator(Hilo hilo)
        {
            _hilo = hilo;
        }

        public override Result Handle() {
            if(!_hilo.EstaActivo) return HilosFailures.HILO_INACTIVO;

            return base.Handle();
        }
    }
}