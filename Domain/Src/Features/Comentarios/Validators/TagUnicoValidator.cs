using Domain.Comentarios.Failures;
using Domain.Comentarios.ValueObjects;
using Domain.Hilos;
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
    

    public class TagUnicoRequeridoValidator :ValidationHandler{
        private readonly ConfiguracionDeComentarios configuracion;
        private readonly TagUnico? tag;
        public TagUnicoRequeridoValidator(ConfiguracionDeComentarios configuracion, TagUnico? tag)
        {
            this.configuracion = configuracion;
            this.tag = tag;
        }

        public override Result Handle()
        {
            if(tag is null && configuracion.IdUnicoActivado){
                return ComentariosFailures.TAG_UNICO_REQUERIDO;
            } else if(tag is not null && !configuracion.IdUnicoActivado){
                return ComentariosFailures.TAG_UNICO_NO_REQUERIDO;
            }
            return base.Handle();
        }
    }
}