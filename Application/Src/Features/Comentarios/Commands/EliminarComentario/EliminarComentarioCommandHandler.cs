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
    public class EliminarComentarioCommandHandler : ICommandHandler<EliminarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public EliminarComentarioCommandHandler(IUnitOfWork unitOfWork, IComentariosRepository comentariosRepository, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _comentariosRepository = comentariosRepository;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(EliminarComentarioCommand request, CancellationToken cancellationToken)
        {

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new ComentarioId(request.Comentario));

            if (comentario is null) return ComentariosFailures.NoEncontrado;

            Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado;

            if (hilo.Id != comentario.Hilo) return ComentariosFailures.NoEncontrado;

            var result = comentario.Eliminar(hilo);

            if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}