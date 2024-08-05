using SharedKernel.Abstractions;

namespace Domain.Categorias
{
    public class Categoria : Entity<CategoriaId> {
        public string Nombre { get; private set; }
        public List<Subcategoria> Subcategorias{ get; private set; }

        public Categoria(string nombre, List<SubcategoriaDto> subcategorias) {
            this.Id = new CategoriaId(Guid.NewGuid());
            this.Nombre = nombre;
            this.Subcategorias = subcategorias.Select(s=> new Subcategoria(
                this.Id,
                s.Nombre,
                s.NombreCorto
            )).ToList();
        }
    }

    public class SubcategoriaDto {
        public string Nombre { get; private set;}
        public string NombreCorto { get; private set;}
        public SubcategoriaDto(string nombre, string nombreCorto)
        {
            Nombre = nombre;
            NombreCorto = nombreCorto;
        }

    }

    public class CategoriaId : EntityId
    {
        public CategoriaId(Guid id) : base(id) { }
    }
}