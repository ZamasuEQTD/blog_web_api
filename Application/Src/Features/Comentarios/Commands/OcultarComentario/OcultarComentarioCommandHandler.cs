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
    public class OcultarComentarioCommandHandler : ICommandHandler<OcultarComentarioCommand>
    {
        private readonly IHilosRepository _hilosRepository;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public OcultarComentarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IComentariosRepository comentariosRepository, IHilosRepository hilosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _comentariosRepository = comentariosRepository;
            _hilosRepository = hilosRepository;
        }

        public async Task<Result> Handle(OcultarComentarioCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.Hilo));

            if (hilo is null) return HilosFailures.NoEncontrado;

            Comentario? comentario = await _comentariosRepository.GetComentarioById(new(request.Comentario));

            if (comentario is null) return ComentariosFailures.NoEncontrado;

            // RelacionDeComentario? relacion = await _comentariosRepository.GetRelacionDeComentario(new(_context.UsuarioId), new(request.Comentario));

            // if (relacion is null)
            // {
            //     relacion = new RelacionDeComentario(
            //         comentario.Id,
            //         new(_context.UsuarioId)
            //     );
            // }

            // var result = comentario.Ocultar(hilo, relacion);

            // if (result.IsFailure) return result.Error;

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }
}