using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Usuarios.Exceptions;
using Domain.Baneos;
using Domain.Baneos.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;

namespace Application.Bneos.Commands
{
    public class BanearUsuarioCommandHandler : ICommandHandler<BanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;

        public BanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IUsuariosRepository usuariosRepository, IBaneosRepository baneosRepository)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _usuariosRepository = usuariosRepository;
            _baneosRepository = baneosRepository;
        }

        public async Task<Result> Handle(BanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepository.GetUsuarioById(new(request.UsuarioId));

            if (usuario is not Anonimo anon) return BaneosFailures.SoloPuedesBanearUsuariosAnonimos;

            Baneo baneo = new(
                new(_context.UsuarioId),
                anon.Id,
                DateTime.MinValue,
                request.Mensaje
            );

            _baneosRepository.Add(baneo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}