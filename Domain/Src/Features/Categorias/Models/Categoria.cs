using Domain.Hilos.Events;
using SharedKernel.Abstractions;

namespace Domain.Categorias
{
    public class Categoria 
    {
        public CategoriaId Id { get; private set; }
        public string Nombre { get; private set; }
        public bool OcultoPorDefecto { get; private set; }
        public ICollection<Subcategoria> Subcategorias { get; private set; } = new List<Subcategoria>();

        private Categoria() { }

        public Categoria(string nombre, bool ocultoPorDefecto, List<Subcategoria> subcategorias)
        {
            this.Id = new CategoriaId(Guid.NewGuid());
            this.Nombre = nombre;
            this.OcultoPorDefecto = ocultoPorDefecto;
            this.Subcategorias = subcategorias;
        }
    }

    public class CategoriaId : EntityId
    {
        public CategoriaId(Guid id) : base(id) { }
    }
}