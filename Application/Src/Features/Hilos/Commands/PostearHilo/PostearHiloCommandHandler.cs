using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Categorias;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.ValueObjects;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands
{
    public class PostearHiloCommandHiloCommandHandler : ICommandHandler<PostearHiloCommand, Guid>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;

        public PostearHiloCommandHiloCommandHandler(IUnitOfWork unitOfWork, IUserContext user, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _user = user;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result<Guid>> Handle(PostearHiloCommand request, CancellationToken cancellationToken)
        {
            Result<Titulo> titulo = Titulo.Create(request.Titulo);

            if (titulo.IsFailure) return titulo.Error;

            Result<Descripcion> descripcion = Descripcion.Create(request.Descripcion);

            if (descripcion.IsFailure) return descripcion.Error;

            Hilo hilo = new Hilo(
                titulo.Value,
                descripcion.Value,
                new UsuarioId(_user.UsuarioId),
                new SubcategoriaId(request.Subcategoria),
                null,
                new(
                    request.DadosActivados,
                    request.IdUnicoAtivado
                )
            );

            _hilosRepository.Add(hilo);

            await _unitOfWork.SaveChangesAsync();

            return hilo.Id.Value;
        }
    }
}