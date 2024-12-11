using SharedKernel.Abstractions;

namespace Domain.Categorias {
    public class Subcategoria : Entity<SubcategoriaId>
    {
        public string Nombre { get; private set; }
        public string NombreCorto { get; private set; }
        private Subcategoria() { }
        public Subcategoria(string nombre, string nombreCorto)
        {
            Id = new SubcategoriaId(Guid.NewGuid());
            Nombre = nombre;
            NombreCorto = nombreCorto;
        }
    }

    public class SubcategoriaId : EntityId
    {
        public SubcategoriaId(Guid id) : base(id) { }
    }
}