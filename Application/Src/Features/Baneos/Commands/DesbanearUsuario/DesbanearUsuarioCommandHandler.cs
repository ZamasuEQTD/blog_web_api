using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Baneos;
using Domain.Baneos.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;

namespace Application.Bneos.Commands
{
    public class DesbanearUsuarioCommandHandler : ICommandHandler<DesbanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DesbanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository, IBaneosRepository baneosRepository)
        {
            _unitOfWork = unitOfWork;
            _usuariosRepository = usuariosRepository;
            _baneosRepository = baneosRepository;
        }

        public async Task<Result> Handle(DesbanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepository.GetUsuarioById(new(request.Usuario));

            if (usuario is not Anonimo) return BaneosFailures.SoloPuedesBanearUsuariosAnonimos;

            foreach (var baneo in await _baneosRepository.GetBaneos(new UsuarioId(request.Usuario)))
            {
                baneo.Eliminar();
            }

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }

    static public class BaneosFailures
    {
        static public readonly Error SoloPuedesBanearUsuariosAnonimos = new Error("");
    }
}