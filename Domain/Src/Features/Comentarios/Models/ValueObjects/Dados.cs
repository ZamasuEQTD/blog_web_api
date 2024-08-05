using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.ValueObjects
{
    public class Dados : ValueObject {
        public static readonly int MAX = 6;
        public static readonly int MIN = 1;

        public int Value {get;private set;}

        private Dados(){ 
        }

        private Dados (int value) {
            Value = value;
        }

        static public  Dados Create(int valor){
            CheckRule(new ValidarValorDeDadosRule(valor));
            return new Dados(valor);
        }

        static public bool ValorEsValido(int valor) => valor < MAX && valor> MIN;
        static public bool ValorEsInvalido(int valor)=> !ValorEsValido(valor);
        protected override IEnumerable<object> GetAtomicValues()
        {
            return new object[]{
                this.Value
            };
        }
    }

    public class ValidarValorDeDadosRule : IBusinessRule {
        private readonly int _valor;

        public ValidarValorDeDadosRule(int valor)
        {
            _valor = valor;
        }

        public string Message => throw new NotImplementedException();

        public bool IsBroken() => Dados.ValorEsInvalido(_valor);
    }
}