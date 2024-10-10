namespace Domain.Categorias.Abstractions
{
    public interface ICategoriasRepository
    {
        public void Add(Categoria categoria);
        public void Add(Subcategoria categoria);
        public Task<List<SubcategoriaId>> GetSubcategoriasParanormales();

    }
}