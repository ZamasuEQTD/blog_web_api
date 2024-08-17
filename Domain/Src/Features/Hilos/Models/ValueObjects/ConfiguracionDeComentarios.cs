using SharedKernel;

namespace Domain.Hilos.ValueObjects
{
    public class ConfiguracionDeComentarios
    {
        public bool Dados { get; private set; }
        public bool IdUnicoActivado { get; private set; }
        public ConfiguracionDeComentarios(bool dados, bool idUnicoActivado)
        {
            Dados = dados;
            IdUnicoActivado = idUnicoActivado;
        }

    }
}