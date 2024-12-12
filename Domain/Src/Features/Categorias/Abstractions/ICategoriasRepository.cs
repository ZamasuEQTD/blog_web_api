namespace Domain.Categorias.Abstractions
{
    public interface ICategoriasRepository
    {
        void Add(Categoria categoria);
        void Add(Subcategoria categoria);
        Task<List<SubcategoriaId>> GetSubcategoriasParanormales();
        Task<Subcategoria?> GetSubcategoria(SubcategoriaId subcategoriaId);
    }
}