using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Comentarios.Abstractions;
using Domain.Comentarios.Services;
using Domain.Comentarios.Services.Strategies;
using Domain.Comentarios.Validators;
using Domain.Comentarios.ValueObjects;
using Domain.Common.Abstractions;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Hilos.Failures;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Comentarios.Commands {
    public class ComentarCommandHandler : ICommandHandler<ComentarCommand> {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IHilosRepository _hilosRepository;
        private readonly IUserContext _userContext;
        private readonly IComentariosRepository _comentariosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly LanzadorDeDados _lanzadorDeDados;
        private readonly TagGenerator _tagGenerator;
        public ComentarCommandHandler(IHilosRepository hilosRepository, IUnitOfWork unitOfWork, IUserContext userContext, IComentariosRepository comentariosRepository, IUsuariosRepository usuariosRepository, LanzadorDeDados lanzadorDeDados, TagGenerator tagGenerator)
        {
            _hilosRepository = hilosRepository;
            _unitOfWork = unitOfWork;
            _userContext = userContext;
            _comentariosRepository = comentariosRepository;
            _usuariosRepository = usuariosRepository;
            _lanzadorDeDados = lanzadorDeDados;
            _tagGenerator = tagGenerator;
        }

        public async Task<Result> Handle(ComentarCommand request, CancellationToken cancellationToken)
        {
            Hilo? hilo = await _hilosRepository.GetHiloById(new(request.HiloId));

            if (hilo is null) return HilosFailures.HILO_INEXISTENTE;
            
            IComentarStrategy strategy;

            if(_userContext.Rango == Usuario.RangoDeUsuario.Moderador){
                strategy = new AnonimoComentarStrategy(
                    new InformacionDeComentarioGenerador(
                        _lanzadorDeDados,
                        _tagGenerator,
                        new UsuarioId(_userContext.UsuarioId)
                    )
                );
            } else {
                strategy = new  ModeradorComentarStrategy(
                    new InformacionDeComentarioGenerador(
                        _lanzadorDeDados,
                        _tagGenerator,
                        new UsuarioId(_userContext.UsuarioId)
                    )
                );
            }

            var result = strategy.Comentar(
                new(_userContext.UsuarioId),
                request.Texto,
                hilo
            );

            if(result.IsFailure) return result.Error;

            _comentariosRepository.Add(result.Value);

            await _unitOfWork.SaveChangesAsync();

            return Result.Success();
        }
    }

}