using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Categorias;
using Domain.Categorias.Abstractions;
using MediatR;
using SharedKernel;

namespace Application.Features.Categorias.Commands.SeedCategorias;

public class SeedCategoriasCommandHandler : ICommandHandler<SeedCategoriasCommand>
{
    private readonly ICategoriasRepository _categoriaRepository;
    
    private readonly IUnitOfWork _unitOfWork;
    public SeedCategoriasCommandHandler(ICategoriasRepository categoriaRepository, IUnitOfWork unitOfWork)
    {
        _categoriaRepository = categoriaRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<Result> Handle(SeedCategoriasCommand request, CancellationToken cancellationToken)
    {
         List<Categoria> categorias = new List<Categoria>(){
                new Categoria("General", false, new List<Subcategoria>(
                    ){
                        new Subcategoria("General", "Gen"),
                        new Subcategoria("Consejos", "Con"),
                        new Subcategoria("Preguntas", "Preg"),
                        new Subcategoria("Anecdotas", "ANC"),
                    }),
                    new Categoria("Entretenimiento", false, new List<Subcategoria>(
                    ){
                        new Subcategoria("Humor", "Ent"),
                        new Subcategoria("Tv", "TV"),
                        new Subcategoria("Música", "Mus"),
                        new Subcategoria("Juegos", "Jue"),
                        new Subcategoria("Deportes", "Dep"),
                    }),
                    new Categoria("NSFW", false, new List<Subcategoria>(){
                        new Subcategoria("NSFW", "NSFW")
                    }),
                    new Categoria("Tecnología", false, new List<Subcategoria>(){
                        new Subcategoria("Tecnología", "Tec"),
                        new Subcategoria("Programación", "Prog"),
                        new Subcategoria("Descargas", "Desc"),
                        new Subcategoria("Inteligencia Artificial", "IA")
                    })
            };


            foreach (var categoria in categorias)
            {
                _categoriaRepository.Add(categoria);
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
    }
}
      