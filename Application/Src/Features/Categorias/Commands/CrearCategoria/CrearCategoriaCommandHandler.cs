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
                request.Nombre
            );

            _categoriasRepository.Add(categoria);

            foreach (var s in request.Subcategorias)
            {
                Subcategoria subcategoria = new Subcategoria(
                    categoria.Id,
                    s.Nombre,
                    s.NombreCorto
                );
                _categoriasRepository.Add(subcategoria);
            }

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}