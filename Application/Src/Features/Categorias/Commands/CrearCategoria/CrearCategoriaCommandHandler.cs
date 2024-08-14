using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Categorias;
using Domain.Categorias.Abstractions;
using SharedKernel;

namespace Application.Categorias.Commands
{
    public class CrearCategoriaCommandHandler : ICommandHandler<CrearCategoriaCommand>
    {
        private readonly ICategoriasRepository _categoriasRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CrearCategoriaCommandHandler(IUnitOfWork unitOfWork, ICategoriasRepository categoriasRepository)
        {
            _unitOfWork = unitOfWork;
            _categoriasRepository = categoriasRepository;
        }

        public async Task<Result> Handle(CrearCategoriaCommand request, CancellationToken cancellationToken)
        {
            Categoria categoria = new Categoria(
                request.Nombre,
                request.Subcategorias.Select(
                    s => new SubcategoriaDto(
                        s.Nombre,
                        s.NombreCorto
                    )
                ).ToList()
            );

            _categoriasRepository.Add(categoria);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}