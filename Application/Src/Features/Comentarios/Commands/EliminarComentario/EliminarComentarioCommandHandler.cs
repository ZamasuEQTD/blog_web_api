using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Comentarios.Failures;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using MediatR;
using SharedKernel;

namespace Application.Comentarios.Commands {
    public class EliminarComentarioCommandHandler : ICommandHandler<EliminarComentarioCommand> {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        public EliminarComentarioCommandHandler(IHilosRepository hilosRepository, IComentariosRepository comentariosRepository, IUnitOfWork unitOfWork)
        {
            _hilosRepository = hilosRepository;
            _comentariosRepository = comentariosRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(EliminarComentarioCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if(hilo is null) return HilosFailures.HILO_INEXISTENTE;

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));
            
            if(comentario is null) return ComentariosFailures.COMENTARIO_INEXISTENTE;
            
            var result = comentario.Eliminar(hilo);

            if(result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return result;
        }
    }
}  