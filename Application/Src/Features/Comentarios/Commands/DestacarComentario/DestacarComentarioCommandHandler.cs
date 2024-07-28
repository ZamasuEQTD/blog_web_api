using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Comentarios.Commands;
using Domain.Comentarios.Failures;
using Domain.Comentarios.Services.Strategies;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using Domain.Usuarios.Abstractions;
using SharedKernel;

namespace Application.Comentarios.Commands
{
    public class DestacarComentarioCommandHandler : ICommandHandler<DestacarComentarioCommand> {
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _userContext;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IComentariosRepository _comentariosRepository;
        public DestacarComentarioCommandHandler(IHilosRepository hilosRepository, IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository, IUserContext userContext, IComentariosRepository comentariosRepository)
        {
            _hilosRepository = hilosRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _comentariosRepository = comentariosRepository;
        }

        public async Task<Result> Handle(DestacarComentarioCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new (request.Hilo));
            
            if(hilo is null) return HilosFailures.HILO_INEXISTENTE;

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new ComentarioId(request.Comentario));

            if(comentario is null) return ComentariosFailures.COMENTARIO_INEXISTENTE;

            if(hilo.Id != comentario.Id) return ComentariosFailures.COMENTARIO_INEXISTENTE;

            var result = new DestacadorStrategy(
                new (_userContext.UsuarioId),
                await _comentariosRepository.GetCantidadDeComentariosDestacados(hilo.Id)
            ).Destacar(
                comentario,
                hilo
            );

            if(result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}