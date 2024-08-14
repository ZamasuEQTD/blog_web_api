using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Hilos;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;

namespace Application.Comentarios.Commands
{
    public class DestacarComentarioCommandHandler : ICommandHandler<DestacarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUserContext _user;
        private readonly IUnitOfWork _unitOfWork;
        public async Task<Result> Handle(DestacarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (hilo is null || comentario is null) return HilosFailures.NoEncontrado;

            Result result = await comentario.Destacar(hilo, new(_user.UsuarioId), _comentariosRepository);

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}