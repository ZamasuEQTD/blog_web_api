using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Validators {
    public class TagValidator : ValidationHandler
    {
        private readonly string _tag;
        public TagValidator(string tag)
        {
            _tag = tag;
        }

        public override Result Handle() {
            if(!Tag.EsTagValido(_tag)){
                return TagsFailures.TAG_INVALIDO;
            }
            return base.Handle();
        }
    }
}