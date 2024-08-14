using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Comentarios.Commands
{
    public class DenunciarComentarioCommandHandler : ICommandHandler<DenunciarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public DenunciarComentarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(DenunciarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (hilo is null) return HilosFailures.NoEncontrado;

            if (comentario is null) return ComentariosFailures.NoEncontrado;

            var result = await comentario.Denunciar(
                _comentariosRepository,
                new(_context.UsuarioId)
            );

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}