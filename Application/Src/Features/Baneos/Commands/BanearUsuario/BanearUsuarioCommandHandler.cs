using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Application.Usuarios.Exceptions;
using Domain.Baneos;
using Domain.Baneos.Abstractions;
using Domain.Usuarios;
using Domain.Usuarios.Abstractions;
using SharedKernel;
using SharedKernel.Abstractions;

namespace Application.Bneos.Commands
{
    public class BanearUsuarioCommandHandler : ICommandHandler<BanearUsuarioCommand>
    {
        private readonly IBaneosRepository _baneosRepository;
        private readonly IUsuariosRepository _usuariosRepository;
        private readonly IUserContext _context;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IDateTimeProvider _timeProvider;
        public BanearUsuarioCommandHandler(IUnitOfWork unitOfWork, IUserContext context, IUsuariosRepository usuariosRepository, IBaneosRepository baneosRepository, IDateTimeProvider timeProvider)
        {
            _unitOfWork = unitOfWork;
            _context = context;
            _usuariosRepository = usuariosRepository;
            _baneosRepository = baneosRepository;
            _timeProvider = timeProvider;
        }

        public async Task<Result> Handle(BanearUsuarioCommand request, CancellationToken cancellationToken)
        {
            Usuario? usuario = await _usuariosRepository.GetUsuarioById(new(request.UsuarioId));

            if (usuario is not Anonimo anon) return BaneosFailures.SoloPuedesBanearUsuariosAnonimos;

            DateTime? finalizacion = null;

            switch(request.Duracion) {
                case DuracionBaneo.CincoMinutos:
                    finalizacion = _timeProvider.UtcNow.AddMinutes(5);
                    break;
                case DuracionBaneo.UnaSemana:
                    finalizacion = _timeProvider.UtcNow.AddDays(7);
                    break;
                case DuracionBaneo.UnMes:
                    finalizacion = _timeProvider.UtcNow.AddMonths(1);
                    break;
            }


            Baneo baneo = new(
                new(_context.UsuarioId),
                anon.Id,
                finalizacion,
                request.Mensaje
            );

            _baneosRepository.Add(baneo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return Result.Success();
        }
    }
}