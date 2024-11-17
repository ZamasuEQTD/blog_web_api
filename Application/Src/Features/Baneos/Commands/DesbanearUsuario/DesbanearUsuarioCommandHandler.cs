using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Baneos;
using Domain.Baneos.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Bneos.Commands
{
    public class DesbanearUsuarioCommandHandler : ICommandHandler<DesbanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _time;

        public DesbanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUsuariosRepository usuariosRepository, IBaneosRepository baneosRepository, IDateTimeProvider time)
        {
            _unitOfWork = unitOfWork;
            _usuariosRepository = usuariosRepository;
            _baneosRepository = baneosRepository;
        }

        public async Task<Result> Handle(DesbanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepository.GetUsuarioById(new(request.Usuario));

            if (usuario is not Anonimo) return Result.Success();

            foreach (var baneo in await _baneosRepository.GetBaneos(usuario.Id))
            {
                baneo.Eliminar(_time.UtcNow);
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