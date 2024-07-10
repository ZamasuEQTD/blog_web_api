using Domain.Comentarios.Validators;
using SharedKernel;

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

        static public Result<Dados> Create(int valor){
            Result result = new ValorDeDadosValidator(valor).Handle();

            if(result.IsFailure) return result.Error;

            return Result.Success(new Dados(valor));
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
}