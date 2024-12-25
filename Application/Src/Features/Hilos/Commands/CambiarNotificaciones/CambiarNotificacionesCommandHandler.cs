using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Comentarios;
using Domain.Hilos;
using Domain.Hilos.Abstractions;
using SharedKernel;

namespace Application.Hilos.Commands;

public class CambiarNotificacionesHiloCommandHandler : ICommandHandler<CambiarNotificacionesHiloCommand>
{
    private readonly IHilosRepository _repository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserContext _user;
    public CambiarNotificacionesHiloCommandHandler(IUnitOfWork unitOfWork, IHilosRepository repository, IUserContext user )
    {
        _unitOfWork = unitOfWork;
        _repository = repository;
        _user = user;
    }

    public  async Task<Result> Handle(CambiarNotificacionesHiloCommand request, CancellationToken cancellationToken)
    {
        Hilo? hilo = await  _repository.GetHiloById(new(request.HiloId));

        if(hilo is null ) return HilosFailures.NoEncontrado;

        var result = hilo.CambiarNotificaciones(new (_user.UsuarioId));

        if(result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync();
   
        return Result.Success();
    }
}