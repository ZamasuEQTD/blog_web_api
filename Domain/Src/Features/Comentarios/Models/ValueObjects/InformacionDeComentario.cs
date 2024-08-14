using SharedKernel;

namespace Domain.Comentarios.ValueObjects
{
    public class InformacionComentario : ValueObject
    {
        public Tag Tag { get; private set; }
        public Dados? Dados { get; private set; }
        public TagUnico? TagUnico { get; private set; }

        private InformacionComentario() { }

        public InformacionComentario(Tag tag, Dados? dados, TagUnico? tagUnico)
        {
            this.Tag = tag;
            this.Dados = dados;
            this.TagUnico = tagUnico;
        }
        protected override IEnumerable<object> GetAtomicValues() => [Tag, Dados, TagUnico];
    }

}