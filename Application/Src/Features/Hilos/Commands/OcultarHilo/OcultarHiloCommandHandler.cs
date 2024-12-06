using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Hilos.Commands.OcultarHilo;

public class OcultarHiloCommandHandler : ICommandHandler<OcultarHiloCommand>
{
    private readonly IHilosRepository _hilosRepository;   
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _user;
    public OcultarHiloCommandHandler(IHilosRepository hiloRepository, IUnitOfWork unitOfWork, IUserContext user)
    {
        _hilosRepository = hiloRepository;
        _unitOfWork = unitOfWork;
        _user = user;   
    }
    public async Task<Result> Handle(OcultarHiloCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

        if (hilo is null) return HilosFailures.NoEncontrado;

        Result result = hilo.Ocultar(new UsuarioId(_user.UsuarioId));

        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return result;

    }
}