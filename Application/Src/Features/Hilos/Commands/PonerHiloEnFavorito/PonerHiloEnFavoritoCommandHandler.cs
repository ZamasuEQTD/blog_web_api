using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using Domain.Usuarios;
using MediatR;
using SharedKernel;

namespace Application.Hilos.Commands.PonerHiloEnFavorito;

public class PonerHiloEnFavoritoCommandHandler : ICommandHandler<PonerHiloEnFavoritoCommand>
{
    private readonly IHilosRepository _hilosRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _user;
    public PonerHiloEnFavoritoCommandHandler(IHilosRepository hiloRepository, IUnitOfWork unitOfWork, IUserContext user)
    {
        _hilosRepository = hiloRepository;
        _unitOfWork = unitOfWork;
        _user = user;
    }
    public async Task<Result> Handle(PonerHiloEnFavoritoCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await _hilosRepository.GetHiloById(new HiloId(request.Hilo));

        if (hilo is null) return HilosFailures.NoEncontrado;

        Result result = hilo.PonerEnFavoritos(new UsuarioId(_user.UsuarioId));

        if (result.IsFailure) return result.Error;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

}