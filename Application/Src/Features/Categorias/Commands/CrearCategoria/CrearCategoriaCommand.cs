using Application.Abstractions.Messaging;

namespace Application.Categorias.Commands
{
  public class CrearCategoriaCommand : ICommand
  {
    public string Nombre { get; set; }
    public List<CrearSubcategoriaDto> Subcategorias { get; set; }
    public CrearCategoriaCommand(string nombre, List<CrearSubcategoriaDto> subcategorias)
    {
      Nombre = nombre;
      Subcategorias = subcategorias;
    }

  }

  public class CrearSubcategoriaDto
  {
    public string Nombre { get; set; }
    public string NombreCorto { get; set; }

    public CrearSubcategoriaDto(string nombre, string nombreCorto)
    {
      Nombre = nombre;
      NombreCorto = nombreCorto;
    }

  }
}