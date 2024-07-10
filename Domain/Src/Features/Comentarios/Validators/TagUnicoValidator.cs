using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Domain.Comentarios.Validators
{
    public class TagUnicoValidator : ValidationHandler
    {
        private readonly string _tag;

        public TagUnicoValidator(string tag)
        {
            _tag = tag;
        }

        public override Result Handle()
        {
            if(TagUnico.EsTagInvalido(_tag)) return TagsFailures.TAG_UNICO_INVALIDO;

            return base.Handle();
        }
    }
}