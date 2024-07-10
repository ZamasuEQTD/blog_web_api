using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Comentarios.ValueObjects;
using Domain.Common.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Comentarios.Commands
{
    public class ComentarCommandHandler : ICommandHandler<ComentarCommand> {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _userContext;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IDadosGenerator _dadosGenerator;
        private readonly ITagGenerator _tagGenerator;
        public ComentarCommandHandler(IHilosRepository hilosRepository, IUnitOfWork unitOfWork, IUserContext userContext, IComentariosRepository comentariosRepository, IDadosGenerator dadosGenerator, ITagGenerator tagGenerator)
        {
            _hilosRepository = hilosRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _comentariosRepository = comentariosRepository;
            _dadosGenerator = dadosGenerator;
            _tagGenerator = tagGenerator;
        }

        public async Task<Result> Handle(ComentarCommand request, CancellationToken cancellationToken) {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.HiloId));

            if(hilo is null) return HilosFailures.HILO_INEXISTENTE;


        

            var comentario = Comentario.Create(
                new ComentarioId(Guid.NewGuid()),
                new UsuarioId(_userContext.UsuarioId),
                hilo,
                new InformacionComentario(
                    _tagGenerator.GenerarTag(),
                    hilo.Configuracion.Dados? _dadosGenerator.TirarDados() : null,
                    hilo.Configuracion.IdUnicoActivado? _tagGenerator.GenerarTagUnico(hilo.Id, new(Guid.NewGuid())) : null
                ),
                request.Texto
            );

            if(comentario.IsFailure) return comentario.Error;

            _comentariosRepository.Add(comentario.Value);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

    

   
}