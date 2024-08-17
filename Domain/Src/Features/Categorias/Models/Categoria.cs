using SharedKernel.Abstractions;

namespace Domain.Categorias
{
    public class Categoria : Entity<CategoriaId>
    {
        public string Nombre { get; private set; }
        public bool OcultoPorDefecto { get; private set; }

        public Categoria(string nombre)
        {
            this.Id = new CategoriaId(Guid.NewGuid());
            this.Nombre = nombre;
        }
    }

    public class CategoriaId : EntityId
    {
        public CategoriaId(Guid id) : base(id) { }
    }
}