using SharedKernel.Abstractions;

namespace Domain.Categorias
{
    public class Subcategoria : Entity<SubcategoriaId> {
        public CategoriaId Categoria { get; private set; }
        public string Nombre { get; private set; }
        public string NombreCorto { get; private set; }

        public Subcategoria(CategoriaId categoria, string nombre, string nombreCorto)
        {
            Categoria = categoria;
            Nombre = nombre;
            NombreCorto = nombreCorto;
        }
    }

    public class SubcategoriaId : EntityId {
        public SubcategoriaId(Guid id) : base(id) {}
    }
}