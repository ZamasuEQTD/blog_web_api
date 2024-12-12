using Domain.Categorias;
using Domain.Comentarios.ValueObjects;

namespace Application.Features.Comentarios.Abstractions
{
   public interface IColorService
    {
        Task<Colores> GenerarColor(SubcategoriaId subcategoria);
    }
}