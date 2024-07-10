using Domain.Usuarios;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Hilos
{
    public class ConfiguracionDeComentarios :ValueObject  {
        public bool Dados {get;private set;}
        public bool IdUnicoActivado {get;private set;}
        public ConfiguracionDeComentarios(bool dados, bool idUnicoActivado)
        {
            Dados = dados;
            IdUnicoActivado = idUnicoActivado;
        }
        
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new  object[] { 
                Dados,
                IdUnicoActivado
            };
        }
    }
}