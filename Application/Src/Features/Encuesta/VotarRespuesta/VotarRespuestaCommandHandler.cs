using Application.Abstractions;
using Application.Abstractions.Data;
using Application.Abstractions.Messaging;
using Domain.Encuestas;
using Domain.Encuestas.Abstractions;
using Domain.Usuarios;
using SharedKernel;

namespace Application.Encuesta.VotarRespuesta;

public class VotarRespuestaCommandHandler : ICommandHandler<VotarRespuestaCommand>
{
    private readonly IEncuestasRepository _encuestasRepository;
    private readonly IUserContext _user;
    private readonly IUnitOfWork _unitOfWork;

    public VotarRespuestaCommandHandler(IEncuestasRepository encuestasRepository, IUserContext user, IUnitOfWork unitOfWork)
    {
        _encuestasRepository = encuestasRepository;
        _user = user;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(VotarRespuestaCommand request, CancellationToken cancellationToken)
    {
        var encuesta = await _encuestasRepository.GetEncuestaById(new EncuestaId(request.EncuestaId));

        if (encuesta is null) return EncuestasFailures.NoEncontrado;

        var result = encuesta.Votar(new UsuarioId(_user.UsuarioId), new RespuestaId(request.RespuestaId));

        if (result.IsFailure) return result;

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }

    
}